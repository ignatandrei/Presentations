<div class="reveal">
			<div class="slides">
				<section>
				<h2>ASP.NET Core/MVC and Knockout.js - introduction</h2>					
					<p>
						<small> <a href="http://msprogrammer.serviciipeweb.ro/">Andrei Ignat</a> / <a href="http://msprogrammer.serviciipeweb.ro/">http://msprogrammer.serviciipeweb.ro/</a></small>
					</p>
				</section>
					
				
				<section>Slide 2</section>
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