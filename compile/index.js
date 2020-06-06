
var DirArchiver = require('dir-archiver');
var folder= '../2016/';
var file ='DepEmpWebApiKnockout';
var archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();

folder='../2019/shorts/';
file='AngLibrary_NPMComponent'

archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();


file = 'AngNetCoreDemo';
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();


file = 'AzureDevOps';
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();

file = 'NetCore3.0WhatsNew';
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();

file = 'NetCoreGlobalTools';
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();


folder='../2020/';
file='Ang8vsAng9'
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();

file='DockerForDevs'
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();

file='NETCore3Plugins'
archive = new DirArchiver(`${folder}${file}`, `../docs/${file}.zip`,[]);
 
// Create the zip file.
archive.createZip();


var folderToc = require("folder-toc");
folderToc("../docs",  {
    name: 'index.html',
    filter: '*.zip',
    title: 'Presentations'
});
