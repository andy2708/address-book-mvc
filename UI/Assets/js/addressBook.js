//This is the dialog window that handles all the work for us.
function addDialog() {
    $("#addEntry").dialog({
        modal: true,
        width: 800,
        height: 600,
        buttons: [{ //add a close button here
            text: "Close",
            click: function () {
                $(this).dialog("close"); //Make sure we actually close it
                resetAddEntryDivToDefault();//Make it nice and neat again
                reloadAddressList();//Reload so the new/edited/deleted data is there
            }
        }]
    })
}
//Reload the address list
function reloadAddressList() {
    $.post("/Address/updateAddressList", {},
        function (data) {
            $("#partialUpdate").html(data);
        });
}

//Add and entry
function addEntry() {
    $.post("/Address/addEntry", {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        phoneType: $("#phoneType").val(),
        phoneNumber: $("#phoneNumber").val()
    },
        function (data) { //response of the post request
            if (data.message == "Success") {
                addressAddedSuccessfully();
            }
            else {
                addressFailedToAdd(data.data);
            }
        });
}
//Show the user an error message
function addressFailedToAdd(errorMessage) {
    $("#addEntryError").css("display", "block");
    $("#addEntryErrorText").html(errorMessage);
}
//show the user a success message
function addressAddedSuccessfully() {
    resetAddEntryDivToDefault();
    $("#addEntryMessage").css("display", "block");
}
//Show the user an error message
function addressFailedToEdit(errorMessage) {
    $("#editEntryError").css("display", "block");
    $("#editEntryErrorText").html(errorMessage);
}
//Show the user a success message
function addressEditedSuccessfully() {
    resetAddEntryDivToDefault();
    $("#editEntryMessage").css("display", "block");
}
//Show the user an error message
function addressFailedToDelete(errorMessage) {
    $("#deleteEntryError").css("display", "block");
    $("#deleteEntryErrorText").html(errorMessage);
}
//Show the user a success message
function addressDeletedSuccessfully() {
    resetAddEntryDivToDefault();
    $("#deleteEntryMessage").css("display", "block");
}
//Reset the dialog back to defaults. This keeps it neat when the user performs subsequent actions
function resetAddEntryDivToDefault() {
    $("#firstName").val("");
    $("#lastName").val("");
    $("#phoneType").val("Please Select");
    $("#phoneNumber").val("");
    $("#btnAdd").css("display", "block");
    $("#btnEdit").css("display", "none");
    $("#btnDelete").css("display", "none");
    $("#addEntryError").css('display','none')
    $("#addEntryMessage").css('display','none')
    $("#editEntryError").css('display','none')
    $("#editEntryMessage").css('display','none')
    $("#deleteEntryError").css('display','none')
    $("#deleteEntryMessage").css('display','none')
}

//This handles the X in the top of the dialog for the worker div. If clicked it clears everything back to default before closing
$(document).on('click', ".ui-dialog-titlebar-close", function () { resetAddEntryDivToDefault(); reloadAddressList(); });

//Get the details of the addres that we will edit/delete.
//It accepts an action (edit/delete/etc)
function getDetailsForEditingEntry(id,action) {
    addDialog();
    $("#hiddenIdField").val(id);
    $.post("/Address/getAddressDetails", {
        id: id
    }, function (data) {
        updateEntryDivWithDetails(data); //This fills the text boxes with required data
        switch (action) { //use a switch statement here for scalability
            case "edit":
                updateEntryDivForEdit();
                break;
            case "delete":
                updateEntryDivForDelete();
                break;
        }
    });
}
//Takes the response from the server and populates the textboxes with the data
function updateEntryDivWithDetails(data) {
    $("#firstName").val(data.firstName);
    $("#lastName").val(data.lastName);
    $("#phoneType").val(data.phoneType);
    $("#phoneNumber").val(data.phoneNumber);

}
//Show the edit button rather then add
function updateEntryDivForEdit() {
    $("#btnAdd").css("display", "none");
    $("#btnEdit").css("display", "block");
}
//Show the delete button rather then add
function updateEntryDivForDelete() {
    $("#btnAdd").css("display", "none");
    $("#btnDelete").css("display", "block");
}

//This happens when the user clicks "save changes"
//Validation, change monitoring etc would go in here
function editEntry() {
    $.post("/Address/editEntry", {
        id: $("#hiddenIdField").val(),//get the id from the hidden field we set eariler
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        phoneType: $("#phoneType").val(),
        phoneNumber: $("#phoneNumber").val()
    }, function (data) {
        if (data.message == "Success") {
            addressEditedSuccessfully();//let the user know it worked or failed
        }
        else {
            addressFailedToEdit(data.data);
        }
    });
}
//this happens when the user clicks "really delete"
function deleteEntry() {
    $.post("/Address/deleteEntry", {
        id: $("#hiddenIdField").val()//get the id from the hidden field we set eariler
    }, function (data) {
        if (data.message == "Success") {
            addressDeletedSuccessfully(); //let the user know it worked or failed
        }
        else {
            addressFailedToDelete(data.data);
        }
    });
}
//this happens when the user clicks the column header to change the sort order
function sortList(column) {
    var direction = "asc"; //set a default asc direction as that is the most common
    //Check to see whether the column clicked matches the current sorted by column.
    //If it does we need to switch directions.
    if (column == $("#hiddenSort").val()) {
        //If its already asc then we set it desc. If not then we leave it as the default asc
        if ($("#hiddenDirection").val() == "asc") {
            direction = "desc";
        }
    }
    //make the post request with the new column and direction to sort by
    $.post("/Address/sortList", {
        column: column,
        direction: direction
    }, function (data) {
        $("#partialUpdate").html(data);//update the data.
    });
}