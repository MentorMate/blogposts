Feature: Login functionality
	As a application user
	In order to use the system
	I want to login with email and password

Scenario: Logging in with invalid credentials
  Given I am at the Account/Login page
  When I fill the account email textbox with value 'incorrect@mail.com'
  And I fill the password textbox with value 'incorrectpassword'
  And I click the login button
  Then a text 'Can't login! Wrong email or password.' should appear in the validation errors region

Scenario: Logging in with valid credentials
  Given I am at the Account/Login page
  When I fill the account email textbox with value 'myname@mymail.com'
  And I fill the password textbox with value 'mypassword'
  And I click the login button
  Then I should be at the home page