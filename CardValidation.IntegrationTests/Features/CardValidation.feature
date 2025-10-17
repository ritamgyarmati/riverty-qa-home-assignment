Feature: Card validation
	API endpoint for credit card data validation either throwing error or providing card type

	Scenario: Valid Visa card returns ok and correct payment type
		Given user has a card with details "John Doe", "4111111111111111", "03/26", "456"
		When we call the card validation endpoint
		Then the response status is 200
		And the response body contains "10"

	Scenario: Valid MasterCard returns ok and correct payment type
		Given user has a card with details "Jane Doe", "5555555555554444", "07/27", "123"
		When we call the card validation endpoint
		Then the response status is 200
		And the response body contains "20"

	Scenario: Valid AmericanExpress returns ok and correct payment type
		Given user has a card with details "John Doe", "378282246310005", "07/27", "1234"
		When we call the card validation endpoint
		Then the response status is 200
		And the response body contains "30"

	Scenario: Empty card details returns error status and error messages
		Given user has a card with details "", "", "", ""
		When we call the card validation endpoint
		Then the response status is 400
		And the response body has error "Cvv is required" for field "Cvv"
		And the response body has error "Date is required" for field "Date"
		And the response body has error "Owner is required" for field "Owner"
		And the response body has error "Number is required" for field "Number"

	Scenario: When providing shorter CVV code relevant error message in response
		Given user has a card with details "John Doe", "4111111111111111", "03/26", "12"
		When we call the card validation endpoint
		Then the response status is 400
		And the response body has error "Wrong cvv" for field "Cvv"

	Scenario: When providing wrong format CVV code relevant error message in response
		Given user has a card with details "John Doe", "4111111111111111", "03/26", "gggg"
		When we call the card validation endpoint
		Then the response status is 400
		And the response body has error "Wrong cvv" for field "Cvv"

	Scenario: When providing expired date relevant error message in response
		Given user has a card with details "John Doe", "4111111111111111", "03/25", "123"
		When we call the card validation endpoint
		Then the response status is 400
		And the response body has error "Wrong date" for field "Date"

	Scenario: When providing wrong format date relevant error message in response
		Given user has a card with details "John Doe", "4111111111111111", "3/27", "123"
		When we call the card validation endpoint
		Then the response status is 400
		And the response body has error "Wrong date" for field "Date"

	Scenario: When providing wrong card number relevant error message in response
		Given user has a card with details "John Doe", "41111111111111", "03/27", "123"
		When we call the card validation endpoint
		Then the response status is 400
		And the response body has error "Wrong number" for field "Number"

	Scenario: When providing wrong owner relevant error message in response
		Given user has a card with details "333", "4111111111111111", "03/27", "123"
		When we call the card validation endpoint
		Then the response status is 400
		And the response body has error "Wrong owner" for field "Owner"