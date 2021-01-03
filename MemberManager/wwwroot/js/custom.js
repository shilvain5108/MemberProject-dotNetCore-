
$(function () {

   
});

var FormDataSerializeJsonString = function (thisForm) {

    var formJsonString = thisForm.serialize();

    var tempStr1 = "testEzxc1=test&";
    var tempStr2 = "&testEzxc2=test";

    return tempStr1 + formJsonString + tempStr2;
}







