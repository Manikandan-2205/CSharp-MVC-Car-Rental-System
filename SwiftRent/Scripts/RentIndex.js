$(document).ready(function () {

    // Get Car Number in Selection Bar
    getCar();

    // Disable input fields on page load
    load();

    // Handle Save button click
    $("#Save").click(function () {
        if ($("#popupForm").valid()) {
            saveRental();
        }
    });

    // Get Car Number in Selection Bar
    function getCar() {
        $.ajax({
            type: 'GET',
            url: '/Rent/GetCar',
            dataType: 'JSON',
            success: function (data) {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    $("#CarId").append($("<option/>", {
                        text: data[i].CarNo
                    }));
                }
            }
        });
    }

    // Disable input fields
    function load() {
        $("#CustId, #CustName, #Fee, #StartDate, #EndDate").attr("disabled", "disabled");
    }

    // Handle CarId selection change
    $("#CarId").change(function () {
        available();
    });

    // Check if a car is available
    function available() {
        $.ajax({
            type: 'POST',
            url: '/Rent/GetAvil?CarNo=' + $("#CarId").val(),
            dataType: 'JSON',
            success: function (data) {
                console.log(data);

                var avail = data;

                if (avail === "Yes") {
                    enableInputs();
                } else {
                    disableInputs();
                }
            }
        });
    }

    // Enable input fields
    function enableInputs() {
        $("#CustId, #CustName, #Fee, #StartDate, #EndDate").removeAttr('disabled');
    }

    // Disable input fields
    function disableInputs() {
        $("#CustId, #CustName, #Fee, #StartDate, #EndDate").attr("disabled", "disabled");
        alert("This Car is Courently Unavailabel.")
    }

    // Handle CustomerId input
    getCustomerName();
    function getCustomerName() {
        $("#CustId").keyup(function (id) {
            $.ajax({
                type: 'POST',
                url: '/Rent/GetId?id=' + $("#CustId").val(),
                dataType: 'JSON',
                success: function (data) {
                    console.log(data);
                    $("#CustName").val(data);
                }
            });
        });
    }

    // Handle CustomerName input
    getCustomerId();
    function getCustomerId() {
        $("#CustName").keyup(function (name) {
            $.ajax({
                type: 'POST',
                url: '/Rent/GetName?Name=' + $("#CustName").val(),
                dataType: 'JSON',
                success: function (data) {
                    console.log(data);
                    $("#CustId").val(data);
                }
            });
        });
    }

    // Save Rental data
    function saveRental() {
        var formData = {
            CarId: $("#CarId").val(),
            CustId: $("#CustId").val(),
            CustName: $("#CustName").val(),
            Fee: $("#Fee").val(),
            StartDate: $("#StartDate").val(),
            EndDate: $("#EndDate").val()
        };

        $.ajax({
            type: 'POST',
            url: '/Rent/Save',
            data: formData,
            success: function (result) {
                console.log("Data saved successfully");
                alert("Data saved successfully!");
                location.reload();               
            },
            error: function (xhr, status, error) {
                console.error("Error saving data: " + error);
            }
        });
    }
});
