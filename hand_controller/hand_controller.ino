#include <LobotServoController.h>

#define rxPin 10
#define txPin 11

SoftwareSerial mySerial(rxPin, txPin);
LobotServoController myse(mySerial);

// the servo ID(s) are to be kept consistent with the fingers array indecies.
// such that the index into the array is always one less than the ID.
// i.e. the pinky ID is 1 and the index into the fingers array is 0, pinky ID - 1
const int N_FINGERS = 5;

const int PINKY_ID = 1;
const int RING_ID = 2;
const int MIDDLE_ID = 3;
const int INDEX_ID = 4;
const int THUMB_ID = 5;

const int MIN_PINKY_POS = 1900;
const int MAX_PINKY_POS = 1100;

const int MIN_RING_POS = 850;
const int MAX_RING_POS = 1800;

const int MIN_MIDDLE_POS = 900;
const int MAX_MIDDLE_POS = 1900;

const int MIN_INDEX_POS = 1000;
const int MAX_INDEX_POS = 2000;

const int MIN_THUMB_POS = 2000;
const int MAX_THUMB_POS = 900;

const float TICK_SPEED = 1.3;

const int MIN_FINGER_POS = 0;
const int MAX_FINGER_POS = 11;
const int N_INCREMENTS = 12;

struct Finger {
  LobotServo servo; // ID and Position
  int minServoPos;
  int maxServoPos;
  int fingerPos; // integer between 0 and 11 inclusive
  int incrementSize; // maxServoPos - minServoPos / 12
};

struct Gesture {
  int pinkyPos;
  int ringPos;
  int middlePos;
  int indexPos;
  int thumbPos;
};

byte COMMUNICATION_DICTIONARY[256];

// To add a gesture:
// 1) increment N_GESTURES
// 2) add a const gesture
// 3) add the const gesture to the GESTURES array
// 4) add an entry to the COMMUNICATION_DICTIONARY 
// 5) add a case to the loop function
const int N_GESTURES = 10;
const Gesture defaultGesture = (Gesture) {11,11,11,11,11};
const Gesture middleFingerGesture = (Gesture) {0,0,11,0,0};
const Gesture thumbsUpGesture = (Gesture) {0,0,0,0,11};
const Gesture pinkysOutGesture = (Gesture) {11,0,0,0,0};
const Gesture ringOnItGesture = (Gesture) {0,11,0,0,0};
const Gesture rudePointingGesture = (Gesture) {0,0,0,11,0};
const Gesture powerFistGesture = (Gesture) {0,0,0,0,0};
const Gesture spiderManGesture = (Gesture) {11,0,0,11,11};
const Gesture theStinkGesture = (Gesture) {11,0,11,11,0};
const Gesture hangLooseGesture = (Gesture) {11,0,0,0,11};

Gesture GESTURES[N_GESTURES];
Finger fingers[N_FINGERS];
int DELAYS[9];

int matchesGesture(int pinkyPos, int ringPos, int middlePos, int indexPos, int thumbPos) {
  int i = 0;
  for (i = 0; i < N_GESTURES; i++) {
    if (pinkyPos == GESTURES[i].pinkyPos &&
        ringPos == GESTURES[i].ringPos &&
        middlePos == GESTURES[i].middlePos &&
        indexPos == GESTURES[i].indexPos &&
        thumbPos == GESTURES[i].thumbPos) {
          return i;
        }
  }
  return -1;
}

int findExpectedTime(int oldServoPosition, int newServoPosition) {
   float tickSpeed = 1.25; // 1.25 ticks/ms
   int nTicks = abs(newServoPosition - oldServoPosition);
   int expectedTime = ((float)nTicks) * TICK_SPEED;
   return expectedTime;
}

const int gestureOffsetIntoCommsDictionary = 100;
int gestureIndexToComms(int gestureIndex) {
  return gestureIndex + gestureOffsetIntoCommsDictionary;
}
int commsToGestureIndex(int comms) {
  return comms - gestureOffsetIntoCommsDictionary;
}

const int delayOffsetIntoCommsDictionary = 201;
int delayIndexToComms(int delayIndex) {
  return delayIndex + delayOffsetIntoCommsDictionary;
}
int commsToDelayIndex(int comms) {
  return comms - delayOffsetIntoCommsDictionary;
}

/*
 * Returns an int with magnitude and direction (one might say a vector) that indicates the direction of the 
 * increment/decrement and the value to increment/decrement by. The direction is encapsulated by the plus/minus sign.
 */
int incrementVector(int incrementSize, int minPos, int maxPos) {
  // the thumb/pinky servo is backwards so we need to increment it differently
  if (minPos > maxPos)
    return -incrementSize;
  else
    return incrementSize;
}

int constrainPosition(int servoPosition, int minPos, int maxPos) {
  // the thumb/pinky servo is backwards so we need to constrain it differently
  if (minPos > maxPos)
    return constrain(servoPosition, maxPos, minPos);
  else
    return constrain(servoPosition, minPos, maxPos);
}

void printFingerPositions(int pinkyPos, int ringPos, int middlePos, int indexPos, int thumbPos) {
  Serial.print(pinkyPos);
  Serial.print(", ");
  Serial.print(ringPos);
  Serial.print(", ");
  Serial.print(middlePos);
  Serial.print(", ");
  Serial.print(indexPos);
  Serial.print(", ");
  Serial.println(thumbPos);
}

void decrementFinger(int fingerId) {
  int i = fingerId - 1;
  int minPos = fingers[i].minServoPos;
  int maxPos = fingers[i].maxServoPos;
  int newServoPos = fingers[i].servo.Position - incrementVector(fingers[i].incrementSize, minPos, maxPos);
  newServoPos = constrainPosition(newServoPos, minPos, maxPos);
  int expectedTime = findExpectedTime(fingers[i].servo.Position, newServoPos);
  fingers[i].servo.Position = newServoPos;
  fingers[i].fingerPos = constrain(fingers[i].fingerPos - 1, MIN_FINGER_POS, MAX_FINGER_POS);

  myse.moveServo(fingerId, newServoPos, expectedTime);
  delay(expectedTime + 50);
  
  int gestureIndex = matchesGesture(fingers[0].fingerPos, fingers[1].fingerPos, fingers[2].fingerPos, fingers[3].fingerPos, fingers[4].fingerPos);
  if (gestureIndex >= 0) {
    int gesture = gestureIndexToComms(gestureIndex);
    Serial.println(gesture);
  }
}

void incrementFinger(int fingerId) {
  int i = fingerId - 1;
  int minPos = fingers[i].minServoPos;
  int maxPos = fingers[i].maxServoPos;
  int newServoPos = fingers[i].servo.Position + incrementVector(fingers[i].incrementSize, minPos, maxPos);
  newServoPos = constrainPosition(newServoPos, minPos, maxPos);
  int expectedTime = findExpectedTime(fingers[i].servo.Position, newServoPos);
  fingers[i].servo.Position = newServoPos;
  fingers[i].fingerPos = constrain(fingers[i].fingerPos + 1, MIN_FINGER_POS, MAX_FINGER_POS);

  myse.moveServo(fingerId, newServoPos, expectedTime);
  delay(expectedTime + 50);
  
  int gestureIndex = matchesGesture(fingers[0].fingerPos, fingers[1].fingerPos, fingers[2].fingerPos, fingers[3].fingerPos, fingers[4].fingerPos);
  if (gestureIndex >= 0) {
    int gesture = gestureIndexToComms(gestureIndex);
    Serial.println(gesture);
  }
}

int fingerPosToServoPos(int fingerId, int fingerPos) {
  int i = fingerId - 1;
  int minPos = fingers[i].minServoPos;
  int maxPos = fingers[i].maxServoPos;
  int newFingerPos = constrain(fingerPos, MIN_FINGER_POS, MAX_FINGER_POS);
  int increment = incrementVector(fingers[i].incrementSize, minPos, maxPos);
  int newServoPos = increment * newFingerPos + minPos;
  return constrainPosition(newServoPos, minPos, maxPos);
}

void makeGesture(int* fingerPositions) {
  LobotServo servos[5];
  int i = 0;
  int fingerPos = 0;
  int expectedTime = 0;
  int maxExpectedTime = 1500;
  for (i = 0; i < N_FINGERS; i++) {
    fingerPos = fingerPositions[i];
    servos[i].ID = i + 1;
    servos[i].Position = fingerPosToServoPos(i + 1, fingerPos);
    expectedTime = findExpectedTime(fingers[i].servo.Position, servos[i].Position);
    maxExpectedTime = max(expectedTime, maxExpectedTime);
    fingers[i].servo.Position = servos[i].Position;
    fingers[i].fingerPos = fingerPos;
  }
  
  myse.moveServos(servos, N_FINGERS, maxExpectedTime);
  delay(maxExpectedTime + 50);
}

void cycleFinger(int fingerId) {
  int i = fingerId - 1;
  fingers[i].servo.Position = fingers[i].minServoPos;
  fingers[i].fingerPos = 0;
  myse.moveServo(fingerId, fingers[i].servo.Position, 1500);
  delay(1550);
  
  fingers[i].servo.Position = fingers[i].maxServoPos;
  fingers[i].fingerPos = 11;
  myse.moveServo(fingerId, fingers[i].servo.Position, 1500);
  delay(1550);
}

void setup() {
  COMMUNICATION_DICTIONARY[0] = 0; // NULL
  COMMUNICATION_DICTIONARY[1] = 1; // Pinky plus
  COMMUNICATION_DICTIONARY[2] = 2; // Pinky minus
  COMMUNICATION_DICTIONARY[3] = 3; // ring plus
  COMMUNICATION_DICTIONARY[4] = 4; // rin minus
  COMMUNICATION_DICTIONARY[5] = 5; // middle plus
  COMMUNICATION_DICTIONARY[6] = 6; // middle minus
  COMMUNICATION_DICTIONARY[7] = 7; // index plus 
  COMMUNICATION_DICTIONARY[8] = 8; // index minus
  COMMUNICATION_DICTIONARY[9] = 9; // thumb plus
  COMMUNICATION_DICTIONARY[10] = 10;  // thumb minus
  
  COMMUNICATION_DICTIONARY[100] = 100; // defaultGesture
  COMMUNICATION_DICTIONARY[101] = 101; // middleFingerGesture
  COMMUNICATION_DICTIONARY[102] = 102; // thumbsUpGesture
  COMMUNICATION_DICTIONARY[103] = 103; // pinkysOutGesture
  COMMUNICATION_DICTIONARY[104] = 104; // ringOnItGesture
  COMMUNICATION_DICTIONARY[105] = 105; // rudePointingGesture
  COMMUNICATION_DICTIONARY[106] = 106; // powerFistGesture
  COMMUNICATION_DICTIONARY[107] = 107; // spiderManGesture
  COMMUNICATION_DICTIONARY[108] = 108; // theStinkGesture
  COMMUNICATION_DICTIONARY[109] = 109; // hangLooseGesture
  
  COMMUNICATION_DICTIONARY[201] = 201; // pause 1.0 second
  COMMUNICATION_DICTIONARY[202] = 202; // pause 1.5 second
  COMMUNICATION_DICTIONARY[203] = 203; // pause 2.0 second
  COMMUNICATION_DICTIONARY[204] = 204; // pause 2.5 second
  COMMUNICATION_DICTIONARY[205] = 205; // pause 3.0 second
  COMMUNICATION_DICTIONARY[206] = 206; // pause 3.5 second
  COMMUNICATION_DICTIONARY[207] = 207; // pause 4.0 second
  COMMUNICATION_DICTIONARY[208] = 208; // pause 4.5 second
  COMMUNICATION_DICTIONARY[209] = 209; // pause 5.0 second
  COMMUNICATION_DICTIONARY[210] = 210; // pause 10.0 second
  COMMUNICATION_DICTIONARY[211] = 211; // pause 11.0 second
  COMMUNICATION_DICTIONARY[212] = 212; // pause 12.0 second
  COMMUNICATION_DICTIONARY[213] = 213; // pause 13.0 second
  COMMUNICATION_DICTIONARY[214] = 214; // pause 14.0 second
  COMMUNICATION_DICTIONARY[215] = 215; // pause 15.0 second
  COMMUNICATION_DICTIONARY[216] = 216; // pause 16.0 second
  COMMUNICATION_DICTIONARY[217] = 217; // pause 17.0 second
  COMMUNICATION_DICTIONARY[218] = 218; // pause 18.0 second
  COMMUNICATION_DICTIONARY[219] = 219; // pause 19.0 second
  COMMUNICATION_DICTIONARY[220] = 220; // pause 20.0 second

  GESTURES[0] = defaultGesture;
  GESTURES[1] = middleFingerGesture;
  GESTURES[2] = thumbsUpGesture;
  GESTURES[3] = pinkysOutGesture;
  GESTURES[4] = ringOnItGesture;
  GESTURES[5] = rudePointingGesture;
  GESTURES[6] = powerFistGesture;
  GESTURES[7] = spiderManGesture;
  GESTURES[8] = theStinkGesture;
  GESTURES[9] = hangLooseGesture;

  DELAYS[0] = 1000;
  DELAYS[1] = 1500;
  DELAYS[2] = 2000;
  DELAYS[3] = 2500;
  DELAYS[4] = 3000;
  DELAYS[5] = 3500;
  DELAYS[6] = 4000;
  DELAYS[7] = 4500;
  DELAYS[8] = 5000;
  
  pinMode(13,OUTPUT);
  mySerial.begin(9600);
  Serial.begin(9600);
  digitalWrite(13,HIGH);
  delay(2000);

  fingers[0].servo.ID = PINKY_ID; // check
  fingers[1].servo.ID = RING_ID; // check
  fingers[2].servo.ID = MIDDLE_ID; // check
  fingers[3].servo.ID = INDEX_ID; // check
  fingers[4].servo.ID = THUMB_ID; // check
  
  fingers[0].minServoPos = MIN_PINKY_POS;
  fingers[1].minServoPos = MIN_RING_POS;
  fingers[2].minServoPos = MIN_MIDDLE_POS;
  fingers[3].minServoPos = MIN_INDEX_POS;
  fingers[4].minServoPos = MIN_THUMB_POS;
  
  fingers[0].maxServoPos = MAX_PINKY_POS;
  fingers[1].maxServoPos = MAX_RING_POS;
  fingers[2].maxServoPos = MAX_MIDDLE_POS;
  fingers[3].maxServoPos = MAX_INDEX_POS;
  fingers[4].maxServoPos = MAX_THUMB_POS;

  int i = 0;
  for (i = 0; i < N_FINGERS; i++) {
    fingers[i].servo.Position = fingers[i].minServoPos;
    fingers[i].fingerPos = 0;
    fingers[i].incrementSize = abs(fingers[i].maxServoPos - fingers[i].minServoPos) / N_INCREMENTS;
  }

  int fingerPositions[N_FINGERS];
  for (i = 0; i < N_FINGERS; i++) {
    fingerPositions[i] = 0;
  }
  makeGesture(fingerPositions);
  for (i = 0; i < N_FINGERS; i++) {
    fingerPositions[i] = 11;
  }
  makeGesture(fingerPositions);
}

int comms = 0;

void loop() {
  while(Serial.available()) {
    comms = Serial.parseInt();
    if (comms == 0)
      return;
    switch (comms) {
      
      /* ===== Increment/Decrement ===== */
      case 1:
        incrementFinger(PINKY_ID);
        break;
      case 2:
        decrementFinger(PINKY_ID);
        break;
      case 3:
        incrementFinger(RING_ID);
        break;
      case 4:
        decrementFinger(RING_ID);
        break;
      case 5:
        incrementFinger(MIDDLE_ID);
        break;
      case 6:
        decrementFinger(MIDDLE_ID);
        break;
      case 7:
        incrementFinger(INDEX_ID);
        break;
      case 8:
        decrementFinger(INDEX_ID);
        break;
      case 9:
        incrementFinger(THUMB_ID);
        break;
      case 10:
        decrementFinger(THUMB_ID);
        break;

      /* ===== Gestures ===== */
      case 100:
      case 101:
      case 102:
      case 103:
      case 104:
      case 105:
      case 106:
      case 107:
      case 108:
      case 109: {
        int i = 0;
        int fingerPositions[N_FINGERS];
        Gesture gesture = GESTURES[commsToGestureIndex(comms)];
        int* gestureIndex = &(gesture.pinkyPos);
        for (i = 0; i < N_FINGERS; i++) {
          fingerPositions[i] = gestureIndex[i];
        }
        makeGesture(fingerPositions);
        break;
      }
        
      /* ===== Delays ===== */
      case 201:
      case 202:
      case 203:
      case 204:
      case 205:
      case 206:
      case 207:
      case 208:
      case 209:
        delay(DELAYS[commsToDelayIndex(comms)]);
        break;
      case 210:
      case 211:
      case 212:
      case 213:
      case 214:
      case 215:
      case 216:
      case 217:
      case 218:
      case 219:
      case 220:
        delay((comms - 200) * 1000);
        break;

      default:
        Serial.println(0);
        return;
    }
    Serial.println(comms);
  }
}

