var express = require('express');
var app = express();
var rimraf = require('rimraf');
var fs = require('fs'); 
var dir = require('node-dir');
var Dropbox = require('dropbox').Dropbox;
require('isomorphic-fetch');
var path = require('path');
const childProcess = require('child_process');
var Jimp = require("jimp");
var ffmpeg = require('ffmpeg-stream').ffmpeg;
const { join } = require('path')
const isDirectory = source => lstatSync(source).isDirectory()
const getDirectories = source =>
  readdirSync(source).map(name => join(source, name)).filter(isDirectory)
var bodyParser = require('body-parser')
app.use( bodyParser.json() );       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
  extended: true
})); 
var ffmpegPath = require('@ffmpeg-installer/ffmpeg').path;
var ffmpeg = require('fluent-ffmpeg');
ffmpeg.setFfmpegPath(ffmpegPath);
var command = ffmpeg();
var timemark = null;

app.set('port', (process.env.PORT || 5000));
app.use(express.static(__dirname + '/public'));
app.set('views', __dirname + '/views');
app.set('view engine', 'ejs');
/*
This is the template for the email. When sending the email, the subject + mess +
attatchments are changed. It might be neccessary to put the message in here
but we need to get the text for that message. 
*/
var send = require('gmail-send')({
  user: 'elrow.x.absolut@gmail.com',
  pass: 'Elrow123*',
  to:   'robbiejkatz@gmail.com',

  subject: 'test subject',
  text:    'gmail-send example 1',         
});

/*
change access token to new dropbox account
Should be able to find access token on the developer's pages 
on the dropbox website

*/
var dbx = new Dropbox({ accessToken: '2rsVFLgnT-AAAAAAAAAACDsv2aW4R623Ruk_A3aS_QP38a-9-SYyFcCEsQ2c4uto' });
//var dbx = new Dropbox({ accessToken: 'h94Q9844wSAAAAAAAAAACFqLcbZFWFEBbykr8-44CBjyvNWtlrqPsnAJlX7Nx1y6' });

/*
	busy variable keeps track of whether the image/video processing is finished. 
*/
busy = false;


/*
Busy endpoint to check whether video processing is finished. 
*/
app.get('/busy', function(req,res){
	if(busy){
		res.send('busy');
	}
	else{
		res.send('done');
	}
});

/*Hello to the world :) */
app.get('/', function(req, res) {
	res.send("hello world")
});


/*
	This is the function that processes the images (puts the overlays on) and creates the 
	video file (ogv). The methods should be clear about what they are doing. Only unclear 
	one is the composiste which is the overlay function. Also bare in mind that this function 
	is recursive, hence the line  'createOverlays(i+1,option,file);' which calls this method again. 
*/

var createOverlays = function(i, option,file){
	/*
	 	The number 10, refers to the number of pictures being taken. If changing it, take 
	 	care to change the long composite method, which has the number 10 and 20 in it. 
	 	for example, if 12 pictures are needed, change 10 to 12 and 20 to 24 to have the 
	 	video loop over 12 pngs 3 times. This would however introduce an error in the naming 
	 	convention. '/00'+i.toString' would result in pngs named 0010.png and 0011.png. So 
		 That would need to be fixed too. i.e, try keep it to 10 :P 
		 
		 		
				 Jimp.loadFont(Jimp.FONT_SANS_16_BLACK).then(function (font) {
	*/
  if( i < 10 ) { 
		Jimp.read("./Emails/"+file+"/00"+i.toString()+".png",function(err,img){
		if (err) throw err;
		img.crop(420, 0, 1080, 1080).write("./Emails/"+file+"/00"+i.toString()+".png");		//TODO calibrate for final computer
		Jimp.loadFont("./fonts/font/font.fnt").then(function (font) {
			fs.readFile('./event_setup/event.json', 'utf8', function(err, data) {
				if (err) throw err;
					var obj = JSON.parse(data);
					img.print(font, 30, 30, obj.event).print(font, 30, 80, obj.date).print(font, 30, 130, obj.city). write("./Emails/"+file+"/00"+i.toString()+".png");
			});
			
		});	
		Jimp.read("./overlays/"+option+"/o"+i.toString()+".png",function(err,over){
		if (err) throw err;
			over.resize(500,500).write("./overlays/"+option+"/o"+i.toString()+".png");
			img.composite(over,250,250).write("./Emails/"+file+"/00"+i.toString()+".png").write("./Emails/"+file+"/0"+(i+10).toString()+".png").write("./Emails/"+file+"/0"+(i+20).toString()+".png");
			createOverlays(i+1,option,file);
		});
	});
}
else{
	/*
		video options. 
	*/
		command
			.on('end', function() {
   			console.log('Processing finished !');
   				busy = false;
 			})
			.input("./Emails/"+file+"/%03d.png")
			.inputFPS(10)
			.output("./Emails/"+file+"/video.ogv")
			.output("./Emails/"+file+"/video.avi")
			.outputFPS(10)
			.noAudio()
			.size('600x600')
			.videoBitrate('1024k')
			 .run();
		}
}


/*
	This is the actual endpoint that calls the function that does the image/video processing
*/
app.get('/makeGif',function(req,res){
	busy = true; 
	createOverlays( 0,req.query.option, req.query.file);
	/*
		req.query.* collects the parameter sent via the url
	*/

});

/*
	This endpoint takes in a file name. Reads the JSON file inside. Sends an 
	email to the recipient specified in the file, with the video as an attachment.

	Then it uploads the video + details file to dropbox.

	Then it deletes the folder (if all above was successful).

*/
app.get('/sendData', function(req,res){
	res.send('sending data');
	const folder = req.query.file;
	const filepath = './Emails/'+folder;
	fs.readFile(filepath+'/data.json', 'utf8', function(err, data) {
		if (err) throw err;
			console.log('OK: ');
			console.log(data)
			var obj = JSON.parse(data);

		/*
			Below is where the email is being sent. text + subject would have to be
			changed here		
		*/
		send({  
			subject: 'Elrow x Absolut Cathedral', 
			text: 'Hi ' + obj.Firstname + ', it was great to see you at the Elrow x Absolut party!',
			to: obj.EmailAddress,
			files: [ filepath + '/video.avi' ]
			},function(err,res){
				if(err == null){
					console.log('perfection');
					fs.readFile(filepath+'/data.json', function (err, filecontents) {
   					if (err) {
      					console.log('Error: ', err);
    				}

    				/*
						Uploads files to dropbox
    				*/
   					dbx.filesUpload({ path: '/'+folder+'/data.json', contents: filecontents })
      				.then(function (response) {

      				fs.readFile(filepath+'/video.avi', function (err, filecontents) {
   					if (err) {
      					console.log('Error: ', err);
    				}

   					dbx.filesUpload({ path: '/'+folder+'/video.avi', contents: filecontents })
      				.then(function (response) {

      				/*
						File and its contents deleted here.
      				*/	
      				rimraf(filepath, function () { console.log('done');});

        			console.log(response);
      				}).catch(function (err) {
        			console.log(err);
      				});
    				});


        			console.log(response);
      				}).catch(function (err) {
        			console.log(err);
      				});
    				});
				}
				else{
					console.log('problems galore');
				}
			});
	});
});

app.listen(app.get('port'), function() {
  console.log('Node app is running on port', app.get('port'));
});


