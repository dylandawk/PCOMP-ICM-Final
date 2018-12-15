const int pin01 = 2;
const int pin02 = 3;
const int pin03 = 4;
const int pin04 = 5;
const int pin05 = 6;
const int pin06 = 7;
const int pin07 = 8;

bool buttonPressed = false;
int previousState;
int currentState;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  
  pinMode(pin01, INPUT_PULLUP);
  pinMode(pin02, INPUT_PULLUP);
  pinMode(pin03, INPUT_PULLUP);
  pinMode(pin04, INPUT_PULLUP);
  pinMode(pin05, INPUT_PULLUP);
  pinMode(pin06, INPUT_PULLUP);
  pinMode(pin07, INPUT_PULLUP);


  digitalWrite(pin01, HIGH);
  digitalWrite(pin02, HIGH);
  digitalWrite(pin03, HIGH);
  digitalWrite(pin04, HIGH);
  digitalWrite(pin05, HIGH);
  digitalWrite(pin06, HIGH);
  digitalWrite(pin07, HIGH);


}

void loop() {
  // put your main code here, to run repeatedly:
//  if(digitalRead(pin03) == HIGH){
//    Serial.println("HIGH");
//  }
//  if(digitalRead(pin03) == LOW){
//    Serial.println("LOW");
//  }

//  if(digitalRead(pin02) == HIGH && digitalRead(pin01) == HIGH && digitalRead(pin03) == HIGH) {
//    buttonPressed = false;
//  }

  if(digitalRead(pin01) == LOW) {

//    Serial.println("right");
    
    Serial.write(0);
    delay(20);

  }

  else if(digitalRead(pin02) == LOW) {
    
//    Serial.println("left");
    
    Serial.write(1);
    delay(20);
    
  }

  else if(digitalRead(pin03) == LOW) {
    
    //Serial.println("up");
    
    Serial.write(2);
    delay(20);
    
  }

  else if(digitalRead(pin04) == LOW) {
    
//    Serial.println("down");
    
    Serial.write(3);
    delay(20);
    
  }

  else if(digitalRead(pin05) == LOW) {
    
//    Serial.println("wall 01");
    
    Serial.write(4);
    delay(500);
    
  }


  else if(digitalRead(pin06) == LOW) {
    
//    Serial.println("wall 02");
    
    Serial.write(5);
    delay(20);
    
  }

  else if(digitalRead(pin07) == LOW) {
    
//    Serial.println("wall 03");
    
    Serial.write(6);
    delay(20);
    
  }
  



}
