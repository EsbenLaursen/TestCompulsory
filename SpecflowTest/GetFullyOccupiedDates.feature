Feature: GetFullyOccupiedDates
	In order to see the occupied dates
	As a customer
	I want to have a list of the dates that are occupied

Scenario Outline: Get fully occupied dates
	Given I have typed <startdate> and <enddate>
	And the room from '12/19/2018' to '12/21/2018'
	When I press buttonr
	Then the resultus shokuld be '<outcome>'


Examples: 
	|startdate|enddate|outcome|
	|'12/9/2018'|'12/10/2018'|0|
	|'12/18/2018'|'12/19/2018'|1|
	|'12/18/2018'|'12/22/2018'|3|
	|'12/21/2018'|'12/24/2018'|1|
	|'12/22/2018'|'12/23/2018'|0|
