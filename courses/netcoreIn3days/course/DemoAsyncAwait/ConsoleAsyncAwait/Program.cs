//convert to async
var t = new TwoTasks();

var res = await t.Await2Task();
//.GetAwaiter().GetResult();

WriteLine(res);

