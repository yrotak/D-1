const DOne = require("./DOne_node");
const fs = require("fs");


// fs.writeFileSync("C:\\Users\\PC\\Desktop\\Sans lamodqodozdo.png", DOne.decrypt("lmao", DOne.encrypt("lmao", fs.readFileSync("C:\\Users\\PC\\Desktop\\Sans titre.png"))))
console.log(DOne.decrypt("lmao", DOne.encrypt("lmao", DOne.str2Arr("&é\"'ascii(-è_çà)=~#{[|`\\^@]}^ù*$!:;,?./§%µ¨£"))));
console.log(DOne.encrypt("lmao", DOne.str2Arr("&é\"'ascii(-è_çà)=~#{[|`\\^@]}^ù*$!:;,?./§%µ¨£")));