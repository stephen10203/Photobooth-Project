Setup
1. install the heroku CLI
2. login to heroku
3. I think you need to install Node? 


Clone 


1. navigate to where you want the project in cmd prompt
2.  run cmd “heroic:git clone -a elrow-booth” 
3. navigate into elrow-booth
4. Run cmd “npm install” . some imports will give errors. But this should not be an issue.
5. run command “heroku local” and server should be up. 

Key endpoints

localhost:5000/makeGif?file=1&option=1   => makes a gif from pictures in filename 1 and option 1. filename can be anything option is 1-5.  

localhost:5000/busy  => responds with either "busy" or "done".

localhost:5000/sendData?file=1     => sends email and uploads to dropbox. filename can be anything. 


Gmail + dropbox account details:
Dropbox API access token: h94Q9844wSAAAAAAAAAACFqLcbZFWFEBbykr8-44CBjyvNWtlrqPsnAJlX7Nx1y6
email: absoluttestingelrow@gmail.com
password: thingking

NEW DETAILS (SS, 08-07-2018): 
Dropbox API access token: 2rsVFLgnT-AAAAAAAAAACDsv2aW4R623Ruk_A3aS_QP38a-9-SYyFcCEsQ2c4uto
email: elrow.x.absolut@gmail.com
password: Elrow123*


I've added a batch file that when run starts the server. If stephen can add code to interface with that via unity that would be awesome :) 


