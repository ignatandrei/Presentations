const fs = require('fs');
const path=require('path');

let files= fs.readdirSync("../docs/");
console.log(files.length);
let filesHtml = files.filter( function( filename) {return (filename.indexOf("prez.html")>=0);});
console.log(filesHtml.length);
filesHtml.forEach(v=> fs.unlinkSync(path.join("../docs/",v)));

let filesZip = files.filter( function( filename) {return (filename.indexOf(".zip")>=0);});
filesZip.forEach(v=> fs.unlinkSync(path.join("../docs/",v)));



var DirArchiver = require('dir-archiver');
var folder= '../2016/';
var file ='DepEmpWebApiKnockout';
var fileHtml='presentation/index.html';
var archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);

folder='../2019/shorts/';
file='AngLibrary_NPMComponent'
fileHtml='presentation.html';

archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();


file = 'AngNetCoreDemo';
fileHtml='readme.txt';

archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();


file = 'AzureDevOps';
fileHtml='azureDevOps.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);

// Create the zip file.
archive.createZip();

file = 'NetCore3.0WhatsNew';
fileHtml='presentation.html';  

archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);

// Create the zip file.
archive.createZip();

file = 'NetCoreGlobalTools';
fileHtml='index.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);

// Create the zip file.
archive.createZip();


folder='../2020/';
file='Ang8vsAng9'
fileHtml='presentation/Ang8vsAng9.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();

file='DockerForDevs'
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fileHtml='presentation/Docker.html';  

// Create the zip file.
archive.createZip();
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);

file='NETCore3Plugins'
fileHtml='presentation.html';  

archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();

file='Build2020'
fileHtml='presentation/Build2020.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();

file='NETCoreHealthChecks'
fileHtml='presentation/ASPNetCoreHealthCheck.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();



file='ActvityTracing'
fileHtml='presentation/ActvityTracing.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();

file='TestingWebAPI'
fileHtml='presentation/TestingWebAPI.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();

file='WebAPIBP'
fileHtml='presentation/WebAPIBP.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();

folder='../2020/';
file='NET5'
fileHtml='presentation.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();

folder='../2021/';
file='RoslynSourceCodeGenerators'
fileHtml='presentation/RoslynSourceCodeGenerators.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();


folder='../2021/';
file='PowerAutomateDesktop'
fileHtml='presentation/PowerAutomateDesktop.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();


folder='../2021/';
file='WebAPIReturns'
fileHtml='presentation/WebAPIReturns.html';  
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
fs.copyFileSync(`${folder}${file}/${fileHtml}`, `../docs/${file}prez.html`);
 
// Create the zip file.
archive.createZip();


var folderToc = require("folder-toc");
folderToc("../docs",  {
    name: 'index.html',
    templateDir: path.join(__dirname, 'resources/classic'),
    templateFile: 'index.jst',
    filter: '*prez.html',
    title: 'Presentations'
    
});

fs.copyFileSync(`../docs/index.html`, `../docs/404.html`);
