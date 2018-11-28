Feature: CreateBooking
	In order to create a booking
	As a customer
	I want to be told if the room is booked


Scenario Outline: Create booking
	Given I have entered a <startdate> and <enddate>
	And a room is already booked from '11/21/2018' to '11/23/2018'
	When I press book room
	Then the result should be '<outcome>'

Examples: 
	|startdate|enddate|outcome|
	|'11/9/2018'|'11/10/2018'|true|
	|'11/24/2018'|'11/27/2018'|true|
	|'11/9/2018'|'11/30/2018'|false|
	|'11/9/2018'|'11/21/2018'|false|
	|'11/9/2018'|'11/23/2018'|false|
	|'11/21/2018'|'11/23/2018'|false|
	|'11/21/2018'|'11/21/2018'|false|
	|'11/23/2018'|'11/23/2018'|false|
	|'11/23/2018'|'11/21/2018'|false|
	|'11/22/2018'|'11/26/2018'|false|

