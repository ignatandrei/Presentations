using System.Reactive.Linq;

Console.WriteLine("Hello, World!");
var observable = Observable.Interval(TimeSpan.FromSeconds(2));
observable = observable.Take(10);

using (observable.Subscribe(
    next => Console.WriteLine("received " + next),
        ex => Console.WriteLine("error" + ex.Message),
        () => Console.WriteLine("complete")
    ))
{
    Console.WriteLine("start");
    Console.ReadLine();
}

Console.WriteLine("done");
//https://blog.andrei.rinea.ro/2013/06/01/bing-it-on-reactive-extensions-story-code-and-slides/
//http://rxwiki.wikidot.com/101samples
//https://www.reactiveui.net/reactive-extensions/
//https://livebook.manning.com/book/rx-dot-net-in-action/chapter-4/212
//reading a file
var obsLines = Observable.Generate(
    File.OpenText("a.txt"),
    s => !s.EndOfStream,
    s => s,
    s => s.ReadLine()
    )
    .Take(10)
    .Select((it,index) =>$"line {index}: {it}")

    ;

using (obsLines.Subscribe(
    next => Console.WriteLine("received " + next),
        ex => Console.WriteLine("error" + ex.Message),
        () => Console.WriteLine("complete")
    )
    
    )
{
    Console.WriteLine("start");
    Console.ReadLine();
}