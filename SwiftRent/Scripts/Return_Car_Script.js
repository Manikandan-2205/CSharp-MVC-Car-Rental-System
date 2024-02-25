$(document).ready(function () {
    // Call getCar function when the document is ready
    getCar();

    // Event listener for when the CarNo selection changes
    $("#CarNo").change(function () {
        var carNo = $(this).val();
        // Call getRentalData function to fetch rental data based on the selected car number
        getRentalData(carNo);
    });

    // Event listener for form submission
    $("#popupForm").submit(function (event) {
        event.preventDefault();
        // Check if the form is valid before saving rental data
        if ($("#popupForm").valid()) {
            saveRental(); // Call saveRental function to save rental data
        }
    });

    // Function to fetch car data
    function getCar() {
        $.ajax({
            type: 'GET',
            url: '/ReturnCar/GetCar',
            dataType: 'JSON',
            success: function (data) {
                console.log(data);
                // Populate the CarNo select element with data received from the server
                for (var i = 0; i < data.length; i++) {
                    $("#CarNo").append($("<option/>", {
                        value: data[i].CarNo,
                        text: data[i].CarNo
                    }));
                }
            },
            error: function (error) {
                console.error("Error fetching car data: ", error);
            }
        });
    }

    // Function to fetch rental data based on car number
    function getRentalData(carNo) {
        $.ajax({
            type: 'POST',
            url: '/ReturnCar/GetData',
            data: { CarNo: carNo },
            dataType: 'JSON',
            success: function (data) {
                console.log("Rental data:", data);
                // Populate form fields with rental data
                $("#CustId").val(data[0].CustId);
                // Calculate Elsp and Fine when ReturnDate changes
                $("#ReturnDate").change(function () {
                    calculateElspAndFine(data[0].EndDate);
                });
            },
            error: function (error) {
                console.error("Error fetching rental data: ", error);
            }
        });
    }

    // Handle CarId selection change
    $("#CarNo").change(function () {
        available();
    });

    // Check if a car is available
    function available() {
        $.ajax({
            type: 'POST',
            url: '/Rent/GetAvil?CarNo=' + $("#CarNo").val(),
            dataType: 'JSON',
            success: function (data) {
                console.log(data);

                var avail = data;

                if (avail === "No") {
                    enableInputs();
                } else {
                    disableInputs();
                }
            }
        });
    }

    // Enable input fields
    function enableInputs() {
        $("#CustId, #ReturnDate, #Elsp, #Fine").removeAttr('disabled');
    }

    // Disable input fields
    function disableInputs() {
        $("#CustId, #ReturnDate, #Elsp, #Fine").attr("disabled", "disabled");
    }

    // Function to calculate Elsp and Fine
    function calculateElspAndFine(EndDate) {
        console.log("EndDate received:", EndDate); // Log the received EndDate

        // Extract timestamp from EndDate string
        var timestamp = parseInt(EndDate.match(/\d+/)[0]);

        // Create date object from timestamp and reset time to midnight
        var endDate = new Date(timestamp);
        endDate.setHours(0, 0, 0, 0);

        console.log("endDate:", endDate); // Check if endDate is valid

        // Create date object for ReturnDate and reset time to midnight
        var returnDate = new Date($("#ReturnDate").val());
        returnDate.setHours(0, 0, 0, 0);

        console.log("returnDate:", returnDate); // Check if returnDate is valid

        // Ensure valid dates before calculating elapsed days
        if (!isNaN(returnDate) && !isNaN(endDate)) {
            var elapsedDays = Math.floor((returnDate - endDate) / (1000 * 60 * 60 * 24));
            console.log("Elapsed Days:", elapsedDays); // Check elapsedDays
            $("#Elsp").val(elapsedDays >= 0 ? elapsedDays : 0);

            // Calculate fine based on elapsed days
            var fine = calculateFine(elapsedDays);
            console.log("Fine:", fine); // Check fine
            $("#Fine").val(fine);
        } else {
            console.error("Error saving data: EndDate is undefined or invalid");
        }
    }

    // Function to calculate fine based on elapsed days
    function calculateFine(elapsedDays) {
        return elapsedDays > 0 ? elapsedDays * 1000 : 0;
    }

    // Function to handle form submission and save rental data
    function saveRental() {
        // Get form data
        var formData = {
            CarNo: $("#CarNo").val(),
            ReturnDate: $("#ReturnDate").val(),
            Elsp: $("#Elsp").val(),
            Fine: $("#Fine").val()
        };

        // Send form data to server for saving
        $.ajax({
            type: 'POST',
            url: '/ReturnCar/Save', // URL of the Save action in the ReturnCarController
            data: formData, // Form data to be sent to the server
            success: function (result) {
                console.log("Data saved successfully"); // Log success message
                alert("Data saved successfully!"); // Display alert message
                location.reload(); // Reload the page to reflect changes
            },
            error: function (xhr, status, error) {
                console.error("Error saving data: " + error); // Log error message
                alert("Error saving data. Please try again."); // Display error message to the user
            }
        });
    }
});
