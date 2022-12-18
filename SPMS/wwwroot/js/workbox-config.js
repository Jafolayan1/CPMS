module.exports = {
	globDirectory: 'wwwroot/',
	globPatterns: [
		'**/*.{css,png,woff@8bd4575acf83c7696dc7a14a966660a3,woff2@8bd4575acf83c7696dc7a14a966660a3,eot,eot@,svg,ttf,woff,woff2,85,eot@-i3a2kk,svg@-i3a2kk,ttf@-i3a2kk,woff@-i3a2kk,woff2@-i3a2kk,eot@-fvbane,svg@-fvbane,jpg,js,eot@n1z373,svg@n1z373,ttf@n1z373,woff@n1z373,gif,txt,md,json,docx,pdf,doc}'
	],
	swDest: 'wwwroot\js\sw.js',
	ignoreURLParametersMatching: [
		/^utm_/,
		/^fbclid$/
	]
};