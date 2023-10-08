using ConsoleTest;


#region retry when sure of result
//await EmployeeRetryLengthyTasks.CalculateHistoryOld();
//await EmployeeRetryLengthyTasks.CalculateWithPolly();
#endregion
#region RateLimiting
//await EmployeeRateLimiting.CalculateHistoryOld(4, 234, 579, 3423, 8756, 45, 2);
//await EmployeeRateLimiting.CalculateHistoryPolly(4, 234, 579, 3423, 8756, 45, 2);

#endregion
#region Timeout
await EmpWeather.GetWeather(4, 234, 579, 3423, 8756, 45, 2);

#endregion
