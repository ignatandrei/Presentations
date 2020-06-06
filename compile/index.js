var folderToc = require("folder-toc");
folderToc("../2016",  {
    name: '2016.html',
    filter: '*.html',
    title: 'Presentations'
});
var DirArchiver = require('dir-archiver');
var archive = new DirArchiver('../2016', '../docs/2016.zip',[]);
 
// Create the zip file.
archive.createZip();