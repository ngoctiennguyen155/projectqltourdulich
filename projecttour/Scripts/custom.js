
function msg() {
    alert("Hello world!");
    var daystart = document.getElementById("daystart").value;
    var dayend = document.getElementById("dayend").value;
    var cost = document.getElementById("cost").value;

    var node = document.createElement("LI");
    var textnode = document.createTextNode("From " + dayend + " to " + daystart + " with cost: " + cost + "VND");
    node.appendChild(textnode);
    document.getElementById("myList").appendChild(node);
}
$(document).ready(function () {
    $("#addcost").click(function () {
        var daystart = document.getElementById("daystart").value;
        var dayend = document.getElementById("dayend").value;
        var cost = document.getElementById("cost").value;
        if (!daystart) {
            alert("[error] Day start is empty.");
            return;
        } else if (!dayend) {
            alert("[error] Day end is empty.");
            return;
        } else if (!cost) {
            alert("[error] Cost is empty.");
            return;
        } 
        $("#myList").append("<li><input type='date' value='" + daystart + "' name='cost'/><input type='date' value='" + dayend + "' name='cost'/><input type='number' value='"+cost+"' name='cost'/>From " + daystart + " to " + dayend + " with cost: " + cost + "</li>");
       
    });
    $('#create').click(function () {

        // Get the Login Name value and trim it
        var name = $("#myList").children();
        // Check if empty of not
        if (name.length<1) {
            alert("[error] Cost isn't added.");
            return false;
        }
    });

    //diadiem
    $("#addiadiem").click(function () {
        var pathname = window.location.pathname.split("/").length;
        //alert(window.location.pathname.split("/")[pathname-1]);
        $("#currentidtour").val(window.location.pathname.split("/")[pathname - 1]);
        var iddiadiem = $("#dropdowndiadiem option:selected").val();
        var textdiadim = $("#dropdowndiadiem option:selected").text();
        if (iddiadiem != '0')
        $("#listdiadiem").append("<li>'" + iddiadiem + "-" + textdiadim + "'<input hidden type='text' value='" + iddiadiem + "' name='listPlaces'/></li>");
        $("#dropdowndiadiem").val("0");
    });
    $('#submitaddplace').click(function () {
        
        // Get the Login Name value and trim it
        var name = $("#listdiadiem").children();
        // Check if empty of not
        if (name.length < 1) {
            alert("[error] No place isn't added.");
            return false;
        }
    });
    //// detail doan khách
    $("#addstaff").click(function () {
        
    
        var iddiadiem = $("#dropdownstaffs option:selected").val();
        var textdiadim = $("#dropdownstaffs option:selected").text();
        if (iddiadiem != '0')
            $("#liststaffs").append("<li>'" + iddiadiem + "-" + textdiadim + "'<input hidden type='text' value='" + iddiadiem + "' name='listPlaces'/></li>");
        $("#dropdownstaffs").val("0");
    });
    $("#addcustomer").click(function () {


        var iddiadiem = $("#dropdowncustomers option:selected").val();
        var textdiadim = $("#dropdowncustomers option:selected").text();
        if (iddiadiem != '0')
            $("#listcustomers").append("<li>'" + iddiadiem + "-" + textdiadim + "'<input hidden type='text' value='" + iddiadiem + "' name='listPlaces'/></li>");
        $("#dropdowncustomers").val("0");
    });
    $("#addloaichiphi").click(function () {


        var iddiadiem = $("#dropdownloaichiphis option:selected").val();
        var textdiadim = $("#dropdownloaichiphis option:selected").text();
        if (iddiadiem != '0')
            $("#listloaichiphis").append("<li>'" + iddiadiem + "-" + textdiadim + "'<input hidden type='text' value='" + iddiadiem + "' name='listPlaces'/></li>");
        $("#dropdownloaichiphis").val("0");
    });
});