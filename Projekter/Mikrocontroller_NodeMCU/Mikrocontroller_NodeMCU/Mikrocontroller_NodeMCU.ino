#include <gfxfont.h>
#include <Adafruit_SPITFT_Macros.h>
#include <Adafruit_SPITFT.h>
#include <Adafruit_GrayOLED.h>
#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>

#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define SCREEN_WIDTH 128 // OLED display width, in pixels
#define SCREEN_HEIGHT 64 // OLED display height, in pixels
#define OLED_RESET -1 // Reset pin # (or -1 if sharing arduino rest pin)

Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

const char* ssid = "h5pd091122";
const char* password = "h5pd091122_styrer";

//My Domain name with URL path or IP address with path
const char* serverName = "https://192.168.1.104:45455/api/sale";

//Barcode value
String value = "";

WiFiClient client;
HTTPClient http;

// Username of the person who is going to scan the barcode
String username = "omid0152";

void HttpPost() {
	//Check WiFi connection status  
	if (WiFi.status() == WL_CONNECTED) {

		//// Your Domain name with URL path or IP address with path
		http.begin(client, serverName);

		//// Specify content-type header
		http.addHeader("Content-Type", "application/json");
		
		// Data to send with HTTP POST
		String content = "{\"Barcode\":" + String(value) + ", \"UserId\": \"OV8F2Fjo1D\"}";

		// Send HTTP POST request
		int httpCode = http.POST(content);
		// Get response
		String payload = http.getString();

		Serial.print("HTTP Response code: ");
		// Print HTTP return code
		Serial.println(httpCode);
		// Print response
		Serial.println(payload);

		//Close connection
		http.end();
	}
	else {
		Serial.println("WiFi Disconnected");
	}
}

// the setup function
void setup() {
	Serial.begin(115200);
	WiFi.begin(ssid, password);
	Serial.println("Connecting");
	while (WiFi.status() != WL_CONNECTED) {
		delay(500);
		Serial.print(".");
	}
	Serial.println("");
	Serial.print("Connected to WiFi network with IP Address: ");
	Serial.println(WiFi.localIP());

	// Address 0x3D for 128x64
	if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C))
	{
		Serial.println(F("SSD1306 allocation failed"));
		for (;;);
	}
	//Serial.flush();
	delay(10);
	display.clearDisplay();
}

// the loop function
void loop() {
	if (Serial.available()) {
		// Read barcode from serial
		value = Serial.readStringUntil('\n');
		Serial.println(value);
	}	
	if (value.length() > 1)
	{
		// Display scanned barcode on oled
		display.clearDisplay();
		display.setCursor(0, 0);
		display.setTextSize(2);
		display.setTextColor(WHITE);
		display.println("BARCODE:");
		display.println(value);
		display.display();

		// Send POST Request
		HttpPost();
		
		delay(2000);
		value = "";
	}
}