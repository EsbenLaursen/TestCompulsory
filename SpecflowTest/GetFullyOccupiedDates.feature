Feature: GetFullyOccupiedDates
	In order to see the occupied dates
	As a customer
	I want to have a list of the dates that are occupied

Scenario Outline: Get fully occupied dates
	Given I have typed <startdate> and <enddate>
	And the room from '12/19/2019' to '12/21/2019'
	When I press buttonr
	Then the resultus shokuld be '<outcome>'


Examples: 
	|startdate|enddate|outcome|
	|'12/9/2019'|'12/10/2019'|0|
	|'12/19/2019'|'12/19/2019'|1|
	|'12/17/2019'|'12/22/2019'|3|
	|'12/21/2019'|'12/24/2019'|1|
	|'12/22/2019'|'12/23/2019'|0|
