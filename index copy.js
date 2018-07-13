var express = require('express');
var app = express();
var rimraf = require('rimraf');
var PythonShell = require('python-shell');
var fs = require('fs'); 
var dir = require('node-dir');
var Dropbox = require('dropbox').Dropbox;
var dbx = new Dropbox({ accessToken: 'h94Q9844wSAAAAAAAAAACFqLcbZFWFEBbykr8-44CBjyvNWtlrqPsnAJlX7Nx1y6' });
require('isomorphic-fetch');
var path = require('path');
const childProcess = require('child_process');
var Jimp = require("jimp");
var Sync = require('synchronize');
var send = require('gmail-send')({
  user: 'absoluttestingelrow@gmail.com',
  pass: 'thingking',
  to:   'robbiejkatz@gmail.com',

  subject: 'test subject',
  text:    'gmail-send example 1',         // Plain text
});
// const ffmpegPath = require('@ffmpeg-installer/ffmpeg').path;
// var ffmpeg = require('fluent-ffmpeg');
// ffmpeg.setFfmpegPath(ffmpegPath);
// var command = ffmpeg();
// ffmpeg.getAvailableFormats(function(err, formats) {
//   console.log('Available formats:');
//   console.dir(formats);
// });
// var videoshow = require('videoshow');
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
   
busy = false;

app.set('port', (process.env.PORT || 5000));
app.use(express.static(__dirname + '/public'));

// views is directory for all template files
app.set('views', __dirname + '/views');
app.set('view engine', 'ejs');

app.get('/busy', function(req,res){
	if(busy){
		res.send('busy');
	}
	else{
		res.send('done');
	}
});

app.get('/', function(req, res) {
	busy = true;
 	res.send("hello world");
	PythonShell.run('gif.py', function (err) {
	//  if (err) throw err;
	  console.log('finished');
	  busy = false;
	});
});

app.get('/drop',function(req,res){
  fs.readFile('./Emails/1data/hhh.ogv', function (err, filecontents) {
    if (err) {
      console.log('Error: ', err);
    }
    res.send('hello');
    dbx.filesUpload({ path: '/hhh.ogv', contents: filecontents })
      .then(function (response) {
        console.log(response);
      })
      .catch(function (err) {
        console.log(err);
      });
    });

});

app.get('/sendmail',function(req,res){
	
	send({}, function(err,res){
		console.log('error: '  + err);
	});



	//res.send('sending mail');
	// send({
	// 	subject: 'hello world',
	// 	text: 'woohoooo',
	// 	//files: [ './hhh.mp4' ]
	// }, function(err,res){
	// 	console.log(err);
	// });


	
});
	var ffmpegPath = require('@ffmpeg-installer/ffmpeg').path;
	var ffmpeg = require('fluent-ffmpeg');
	ffmpeg.setFfmpegPath(ffmpegPath);
	var command = ffmpeg();
	var timemark = null;


var createOverlays = function(i, option,file){
  if( i < 10 ) {
		Jimp.read("./Emails/"+file+"/00"+i.toString()+".png",function(err,img0){
		if (err) throw err;
		img0.resize(640,480).write("./Emails/"+file+"/00"+i.toString()+".png");

		Jimp.read("./overlays/"+option+"/o"+i.toString()+".png",function(err,over0){
		if (err) throw err;
			//console.log("./overlays/o"+i.toString()+".png");
			over0.resize(640,480).write("./overlays/"+option+"/o"+i.toString()+".png");
			img0.composite(over0,0,0).write("./Emails/"+file+"/00"+i.toString()+".png").write("./Emails/"+file+"/0"+(i+10).toString()+".png").write("./Emails/"+file+"/0"+(i+20).toString()+".png");
			console.log("hi");
			createOverlays(i+1,option,file);

		});
	});
}
	else{
			command
				 .on('end', function() {
   					 console.log('Processing finished !');
   					 busy = false;
 				 })
			    .input("./Emails/"+file+"/%03d.png")
			    .inputFPS(10)
			    .output("./Emails/"+file+"/outs.ogv")
			    .outputFPS(30)
			    .noAudio()
			    .size('640x480')
			    .videoBitrate('1024k')
			    .run();
			    
			    	}
}
app.get('/makeGif',function(req,res){
	busy = true;
	createOverlays( 0,req.query.option, req.query.file);
});





app.get('/readfile',function(req,res){
	filename = './Emails/1/details.txt';
	fs.readFile(filename, 'utf8', function(err, data) {
	  if (err) throw err;
	  console.log('OK: ' + filename);
	  console.log(data)
	  var obj = JSON.parse(data);
	  res.send(obj.name);
	send({
		subject: obj.name + obj.surname  ,
		text: 'woohoooo',
		to: obj.email
	});
	});
});

app.get('/listDir', function(req,res){
	fs.readdir('./Emails', function (err, files) {
	    if (err) {
	        return console.log('Unable to scan directory: ' + err);
	    } 
	    files.forEach(function (file) {
			if (file.indexOf("data") + 1 ){
			    console.log(file);	
			    filename ='./Emails/'+file +'/details.txt';
			    fs.readFile(filename, 'utf8', function(err, data) {
			  		if (err) throw err;
			  		console.log('OK: ' + filename);
			  		console.log(data)
			  		var obj = JSON.parse(data);
			  		res.send(obj.name);
					send({
						subject: obj.name + obj.surname  ,
						text: 'woohoooo',
						to: obj.email
					},function(err,res){
						if(err == null){
							console.log('perfection');
						}
						else{
							console.log('problems galore');
						}
					});

				});
				//rimraf('./Emails/'+file, function () { console.log('done'); });
			} 
		});
	});
});


app.get('/sendData', function(req,res){
	res.send('sending data');
	const folder = req.query.filepath;
	const filepath = './Emails/'+folder;
  	//var filepath = './Emails/1data';
	fs.readFile(filepath+'/details.txt', 'utf8', function(err, data) {
		if (err) throw err;
			console.log('OK: ');
			console.log(data)
			var obj = JSON.parse(data);
		send({
			subject: obj.name + obj.surname  ,
			text: 'woohoooo',
			to: obj.email,
			files: [ filepath + '/hhh.ogv' ]
			},function(err,res){
				if(err == null){
					console.log('perfection');
					fs.readFile(filepath+'/details.txt', function (err, filecontents) {
   					if (err) {
      					console.log('Error: ', err);
    				}

   					dbx.filesUpload({ path: '/'+folder+'/details.txt', contents: filecontents })
      				.then(function (response) {

      				fs.readFile(filepath+'/hhh.ogv', function (err, filecontents) {
   					if (err) {
      					console.log('Error: ', err);
    				}

   					dbx.filesUpload({ path: '/'+folder+'/hhh.ogv', contents: filecontents })
      				.then(function (response) {

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




// 	var ffmpegPath = require('@ffmpeg-installer/ffmpeg').path;
// 	var ffmpeg = require('fluent-ffmpeg');
// 	ffmpeg.setFfmpegPath(ffmpegPath);
// 	var command = ffmpeg();
// 	var timemark = null;

// command
//     .input('./pics/%03d.png')
//     .inputFPS(5)
//     .output('./Sinewave3-1920x1080.ogv')
//     .outputFPS(30)
//     .noAudio()
//     .size('640x480')
//     .videoBitrate('1024k')
//     .run();
 	

app.listen(app.get('port'), function() {
  console.log('Node app is running on port', app.get('port'));
});


