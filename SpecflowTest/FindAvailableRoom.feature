Feature: FindAvailableRoom
	In order to find out if a room is available
	As a customer
	I want to get the room id


Scenario Outline: Find available room
	Given I have entered <startdate> and <enddate>
	And the room is already booked from '11/21/2018' to '11/23/2018'
	When I press button
	Then the resultus should be '<outcome>'


Examples: 
	|startdate|enddate|outcome|
	|'11/9/2018'|'11/10/2018'|1|
	|'11/24/2018'|'11/27/2018'|1|
	|'11/9/2018'|'11/30/2018'|-1|
	|'11/9/2018'|'11/21/2018'|-1|
	|'11/9/2018'|'11/23/2018'|-1|
	|'11/21/2018'|'11/23/2018'|-1|
	|'11/21/2018'|'11/21/2018'|-1|
	|'11/23/2018'|'11/23/2018'|-1|
	|'11/23/2018'|'11/21/2018'|-1|
	|'11/22/2018'|'11/26/2018'|-1|