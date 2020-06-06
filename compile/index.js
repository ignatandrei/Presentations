const fs = require('fs');
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


var folderToc = require("folder-toc");
folderToc("../docs",  {
    name: 'index.html',
    filter: '*prez.html',
    title: 'Presentations'
});
