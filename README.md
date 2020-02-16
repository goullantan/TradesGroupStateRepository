# TradesGroupStateRepository
Program used to group input trades by correlation Id and attribute a state according to a limit value

There are 4 steps in the output report construction :
1) Getting trades from an xml input file
2) Aggregating this trades by Correlation Id
3) Attributing a state according to the Value/Limit comparison and the Number of Trades
4) Csv report generation with the result

Every steps are independant and are implementing different interfaces.
One of this steps can be changed by another logic, the new class just has to implement the concerned interface.

For exemple, if the state attribution logic has to be changed, you just need to create a new class implementing 
the ITradesGroupStateAttributor interface and change the StatusAttributorInstance name in the App.config.

The class Program will then create the right instance to create the report.
