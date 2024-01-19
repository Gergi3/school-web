(async function initializeSnowstorm() {
	snowStorm.flakesMaxActive = 256;
	snowStorm.autoStart = false;
	snowStorm.useTwinkleEffect = true;
	snowStorm.useMeltEffect = true;
	snowStorm.vMaxX = 2;
	snowStorm.vMaxY = 4;
	snowStorm.flakeWidth = 25;
	snowStorm.flakeHeight = 20;


	await fetch('https://emoji-api.com/emojis?access_key=fcd8eb9e6465aea78765b90289c7bb658b07bf21')
		.then(response => {
			response.json().then(data => {
				snowStorm.snowCharacter = data.map(x => x.character);
				snowStorm.start();
			})
		});

})();

//snowStorm.snowCharacter = emojis;

//setInterval(() => {
//	snowStorm.snowCharacter = emojis[Math.floor(Math.random() * emojis.length)].character;
//	snowStorm.start();
//}, 3000)
