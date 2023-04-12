<div class="reveal">
			<div class="slides">
				<section>
					<div>
<h1> .NET Core What's new</h1>
				</div>
				<ul><li>About me</li>
				<li>C# 8.0</li>
				<li>.NET Core 3.0 </li>
				<li>ASP.NET Core 3.0 </li>
				<li>EF Core 3.0</li>
				<li>Summary</li>
				</ul>
				
					<p>
						<small> <a href="http://msprogrammer.serviciipeweb.ro/">Andrei Ignat</a> / <a href="http://msprogrammer.serviciipeweb.ro/">http://msprogrammer.serviciipeweb.ro/</a></small>
					</p>
				</section>
					
				
				<section># About me<ul><li>
				Andrei Ignat http://msprogrammer.serviciipeweb.ro/</li>
				<li>
www.ASP.NET forum moderator</li>
				<li>
YouTube 5 minutes .NET and tools : http://bit.ly/5MinTools</li>
				<li>
Book Making Open Source Component  : http://bit.ly/NetOpenSourceComponent</li>
				<li>
Book Copy Paste from StackOverflow : https://amzn.to/2PQ8EDc</li>
				<li>
Monthly meetings:  https://www.meetup.com/Bucharest-A-D-C-E-S-Meetup/</li>
				</ul>
				</section>
				
				
			</div>
		</div>
		
		
		<script>
		
		
		
		
		function addCss(url){
			console.log(url);
			var link = document.createElement('link');
			link.setAttribute('rel', 'stylesheet');
			link.setAttribute('type', 'text/css');
			link.setAttribute('href', url);
			document.getElementsByTagName('head')[0].appendChild(link);
		}		
		addCss("https://cdnjs.cloudflare.com/ajax/libs/reveal.js/3.8.0/css/reset.css");
		addCss("https://cdnjs.cloudflare.com/ajax/libs/reveal.js/3.8.0/css/reveal.css");
		addCss("https://cdnjs.cloudflare.com/ajax/libs/reveal.js/3.8.0/css/theme/black.css");
		addCss("https://cdnjs.cloudflare.com/ajax/libs/reveal.js/3.8.0/lib/css/monokai.css");
		</script>
		
		<script src="https://cdnjs.cloudflare.com/ajax/libs/reveal.js/3.8.0/js/reveal.js"></script>
		<script>
			Reveal.initialize();
			var rev= document.getElementsByClassName("reveal")[0];
			var id=document.getElementById("documentation-container").parentElement;
			id.appendChild(rev);
		</script>