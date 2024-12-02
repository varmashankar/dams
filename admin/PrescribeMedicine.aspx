<%@ Page Title="" Language="C#" MasterPageFile="~/admin/admin.master" AutoEventWireup="true" CodeFile="PrescribeMedicine.aspx.cs" Inherits="admin_PrescribeMedicine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <!-- Ensure this line is included -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.5.2/dist/js/bootstrap.bundle.min.js"></script>
    <!-- Bootstrap JS (optional) -->
    <script>
        $(document).ready(function () {
            const txtMedicineName = $("#<%= txtMedicineName.ClientID %>");
            const suggestionsContainer = $("#suggestions");
            const ddlMedicineType = $("#<%= ddlMedicineType.ClientID %>");
            const hfSelectedIndex = $("#<%= hfSelectedIndex.ClientID %>");
            let selectedSuggestionIndex = parseInt(hfSelectedIndex.val()) || -1; // Retrieve the stored index or default to -1
            let medicines = []; // Store fetched medicines for later use

            txtMedicineName.on("keyup", function (event) {
                var input = $(this).val();
                if (input.length >= 1) { // Start searching after 1 character
                    $.ajax({
                        type: "POST",
                        url: "PrescribeMedicine.aspx/GetMedicineNames", // URL for the page method
                        data: JSON.stringify({ prefixText: input }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            medicines = response.d; // Store the fetched medicines
                            suggestionsContainer.empty();
                            selectedSuggestionIndex = -1; // Reset the index on new input

                            $.each(medicines, function (index, medicine) {
                                suggestionsContainer.append("<div class='suggestion-item'>" + medicine.Name + "</div>");
                            });

                            // Show suggestions if there are any
                            if (medicines.length > 0) {
                                suggestionsContainer.show();
                            } else {
                                suggestionsContainer.hide(); // Hide if no suggestions
                            }

                            // Reapply the selected class if index is valid
                            if (selectedSuggestionIndex >= 0 && selectedSuggestionIndex < medicines.length) {
                                highlightSuggestion();
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error("Error fetching medicine names:", error);
                        }
                    });
                } else {
                    suggestionsContainer.hide(); // Hide suggestions if less than 1 character
                }
            });

            // Handle suggestion item click
            $(document).on("click", ".suggestion-item", function () {
                const selectedMedicineName = $(this).text();

                // Find the selected medicine type
                const selectedMedicine = medicines.find(m => m.Name === selectedMedicineName);

                txtMedicineName.val(selectedMedicineName);
                suggestionsContainer.empty(); // Clear suggestions after selection
                suggestionsContainer.hide(); // Hide suggestions after selection

                // Set the medicine type in the dropdown
                if (selectedMedicine) {
                    ddlMedicineType.val(selectedMedicine.Type); // Set the type in the dropdown
                } else {
                    ddlMedicineType.val(''); // Reset if no match
                }
            });

            txtMedicineName.on("keydown", function (event) {
                const items = suggestionsContainer.children('.suggestion-item');
                if (event.key === 'ArrowDown') {
                    event.preventDefault(); // Prevent cursor moving to the end of the input
                    selectedSuggestionIndex = Math.min(selectedSuggestionIndex + 1, items.length - 1);
                    hfSelectedIndex.val(selectedSuggestionIndex); // Store the index
                    highlightSuggestion();
                } else if (event.key === 'ArrowUp') {
                    event.preventDefault(); // Prevent cursor moving to the end of the input
                    selectedSuggestionIndex = Math.max(selectedSuggestionIndex - 1, -1);
                    hfSelectedIndex.val(selectedSuggestionIndex); // Store the index
                    highlightSuggestion();
                } else if (event.key === 'Enter') {
                    event.preventDefault(); // Prevent form submission if within a form
                    if (selectedSuggestionIndex >= 0) {
                        const selectedItem = items.eq(selectedSuggestionIndex);
                        const selectedMedicineName = selectedItem.text();

                        txtMedicineName.val(selectedMedicineName); // Set selected value
                        suggestionsContainer.empty(); // Clear suggestions after selection
                        suggestionsContainer.hide(); // Hide suggestions after selection

                        // Find the selected medicine type
                        const selectedMedicine = medicines.find(m => m.Name === selectedMedicineName);
                        if (selectedMedicine) {
                            ddlMedicineType.val(selectedMedicine.Type); // Set the type in the dropdown
                        }
                    }
                }
            });

            // Function to highlight the current suggestion based on index
            function highlightSuggestion() {
                const items = suggestionsContainer.children('.suggestion-item');
                console.log('Highlighting suggestion at index:', selectedSuggestionIndex);
                if (selectedSuggestionIndex >= 0 && selectedSuggestionIndex < items.length) {
                    items.eq(selectedSuggestionIndex).addClass('selected-item'); // Highlight the current item
                    console.log('Highlighted item:', items.eq(selectedSuggestionIndex).text());
                }
            }
        });
        $(document).ready(function () {
            const txtMedicineName = $("#<%= txtMedicineName.ClientID %>");
    const suggestionsContainer = $("#suggestions");
    const ddlMedicineType = $("#<%= ddlMedicineType.ClientID %>");
    const hfSelectedIndex = $("#<%= hfSelectedIndex.ClientID %>");
    let selectedSuggestionIndex = parseInt(hfSelectedIndex.val()) || -1; // Retrieve the stored index or default to -1
    let medicines = []; // Store fetched medicines for later use

    txtMedicineName.on("keyup", function (event) {
        var input = $(this).val();
        if (input.length >= 1) { // Start searching after 1 character
            $.ajax({
                type: "POST",
                url: "PrescribeMedicine.aspx/GetMedicineNames", // URL for the page method
                data: JSON.stringify({ prefixText: input }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    medicines = response.d; // Store the fetched medicines
                    suggestionsContainer.empty();
                    selectedSuggestionIndex = -1; // Reset the index on new input

                    $.each(medicines, function (index, medicine) {
                        suggestionsContainer.append("<div class='suggestion-item'>" + medicine.Name + "</div>");
                    });

                    // Show suggestions if there are any
                    if (medicines.length > 0) {
                        suggestionsContainer.show();
                    } else {
                        suggestionsContainer.hide(); // Hide if no suggestions
                    }

                    // Reapply the selected class if index is valid
                    if (selectedSuggestionIndex >= 0 && selectedSuggestionIndex < medicines.length) {
                        highlightSuggestion();
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching medicine names:", error);
                }
            });
        } else {
            suggestionsContainer.hide(); // Hide suggestions if less than 1 character
        }
    });

    // Handle suggestion item click
    $(document).on("click", ".suggestion-item", function () {
        const selectedMedicineName = $(this).text();

        // Find the selected medicine type
        const selectedMedicine = medicines.find(m => m.Name === selectedMedicineName);

        txtMedicineName.val(selectedMedicineName);
        suggestionsContainer.empty(); // Clear suggestions after selection
        suggestionsContainer.hide(); // Hide suggestions after selection

        // Set the medicine type in the dropdown
        if (selectedMedicine) {
            ddlMedicineType.val(selectedMedicine.Type); // Set the type in the dropdown
        } else {
            ddlMedicineType.val(''); // Reset if no match
        }
    });

    txtMedicineName.on("keydown", function (event) {
        const items = suggestionsContainer.children('.suggestion-item');
        if (event.key === 'ArrowDown') {
            event.preventDefault(); // Prevent cursor moving to the end of the input
            selectedSuggestionIndex = Math.min(selectedSuggestionIndex + 1, items.length - 1);
            hfSelectedIndex.val(selectedSuggestionIndex); // Store the index
            highlightSuggestion();
        } else if (event.key === 'ArrowUp') {
            event.preventDefault(); // Prevent cursor moving to the end of the input
            selectedSuggestionIndex = Math.max(selectedSuggestionIndex - 1, -1);
            hfSelectedIndex.val(selectedSuggestionIndex); // Store the index
            highlightSuggestion();
        } else if (event.key === 'Enter') {
            event.preventDefault(); // Prevent form submission if within a form
            if (selectedSuggestionIndex >= 0) {
                const selectedItem = items.eq(selectedSuggestionIndex);
                const selectedMedicineName = selectedItem.text();

                txtMedicineName.val(selectedMedicineName); // Set selected value
                suggestionsContainer.empty(); // Clear suggestions after selection
                suggestionsContainer.hide(); // Hide suggestions after selection

                // Find the selected medicine type
                const selectedMedicine = medicines.find(m => m.Name === selectedMedicineName);
                if (selectedMedicine) {
                    ddlMedicineType.val(selectedMedicine.Type); // Set the type in the dropdown
                }
            }
        }
    });

    // Function to highlight the current suggestion based on index
    function highlightSuggestion() {
        const items = suggestionsContainer.children('.suggestion-item');
        console.log('Highlighting suggestion at index:', selectedSuggestionIndex);
        if (selectedSuggestionIndex >= 0 && selectedSuggestionIndex < items.length) {
            items.eq(selectedSuggestionIndex).addClass('selected-item'); // Highlight the current item
            console.log('Highlighted item:', items.eq(selectedSuggestionIndex).text());
        }
    }
});


    </script>
    <style>
        .suggestions-container {
            border: 1px solid #ddd; /* Border around the suggestions container */
            border-radius: 4px; /* Rounded corners */
            background-color: #fff; /* White background */
            max-height: 200px; /* Limit height */
            overflow-y: auto; /* Add scroll if needed */
            position: absolute; /* Position relative to the input field */
            z-index: 1000; /* Ensure it appears above other elements */
            width: calc(50% - 2px); /* Full width minus border */
        }

        .suggestion-item {
            padding: 10px; /* Spacing around each item */
            cursor: pointer; /* Pointer cursor on hover */
        }

            .suggestion-item:hover {
                background-color: #f1f1f1; /* Light gray on hover */
            }

        .selected-item {
            background-color: #e0e0e0; /* Highlight color for selected suggestion */
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container bg-white p-5 shadow-lg rounded mt-4">
    <h2 class="text-center mb-4">Prescribe Medicine</h2>
    <hr class="mb-4" />
    <div id="prescribeForm" runat="server" class="px-5">
        <asp:HiddenField ID="hfAppointmentID" runat="server" />
        <asp:HiddenField ID="hfPatientID" runat="server" />
        <asp:HiddenField ID="hfSelectedIndex" runat="server" />
        <!-- Medicine Name Input -->
        <div class="form-group mb-4">
            <label for="medicineName" class="form-label">
                <i class="fas fa-pills me-2"></i>Medicine Name
            </label>
            <div class="input-group">
                <asp:TextBox ID="txtMedicineName" runat="server" CssClass="form-control" placeholder="Enter Medicine Name" required />
                <span class="input-group-text"><i class="fas fa-capsules"></i></span>
            </div>
            <div id="suggestions" class="suggestions-container" style="display: none;">
                <!-- Suggestions will be appended here -->
            </div>
        </div>


        <!-- Medicine Type Selection -->
        <div class="form-group mb-4">
            <label for="medicineType" class="form-label"><i class="fas fa-clipboard-list me-2"></i>Medicine Type</label>
            <asp:DropDownList ID="ddlMedicineType" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Type" Value="" />
                <asp:ListItem Text="Tablet" Value="Tablet" />
                <asp:ListItem Text="Capsule" Value="Capsule" />
                <asp:ListItem Text="Syrup" Value="Syrup" />
                <asp:ListItem Text="Injection" Value="Injection" />
                <asp:ListItem Text="Ointment" Value="Ointment" />
            </asp:DropDownList>
        </div>

        <!-- Dosage Input -->
        <div class="form-group mb-4">
            <label for="dosage" class="form-label"><i class="fas fa-syringe me-2"></i>Dosage</label>
            <div class="input-group">
                <asp:TextBox ID="txtDosage" runat="server" CssClass="form-control" placeholder="Enter Dosage (e.g., 2 pills twice a day)" required />
                <span class="input-group-text"><i class="fas fa-prescription-bottle-alt"></i></span>
            </div>
        </div>

        <!-- Frequency of Dosage -->
        <div class="form-group mb-4">
            <label for="frequency" class="form-label"><i class="fas fa-clock me-2"></i>Frequency</label>
            <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="form-control">
                <asp:ListItem Text="Select Frequency" Value="" />
                <asp:ListItem Text="Once a day" Value="Once a day" />
                <asp:ListItem Text="Twice a day" Value="Twice a day" />
                <asp:ListItem Text="Three times a day" Value="Three times a day" />
                <asp:ListItem Text="Weekly" Value="Weekly" />
            </asp:DropDownList>
        </div>

        <!-- Duration of Treatment -->
        <div class="form-group mb-4">
            <label for="duration" class="form-label"><i class="fas fa-calendar-alt me-2"></i>Duration</label>
            <div class="input-group">
                <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" placeholder="Enter Duration (e.g., 5 days)" required />
                <span class="input-group-text"><i class="fas fa-calendar-day"></i></span>
            </div>
        </div>

        <!-- Allergy Information -->
        <div class="form-group mb-4">
            <label for="allergies" class="form-label"><i class="fas fa-exclamation-triangle me-2"></i>Allergy Information</label>
            <asp:TextBox ID="txtAllergies" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Enter any allergies (if any)" Rows="2"></asp:TextBox>
        </div>

        <!-- Medicine Timing Input -->
        <div class="form-group mb-4">
            <label for="timing" class="form-label"><i class="fas fa-clock me-2"></i>Medicine Timing</label>
            <div class="form-check">
                <input class="form-check-input" type="checkbox" id="chkMorning" runat="server" />
                <label class="form-check-label" for="chkMorning">Morning</label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="checkbox" id="chkAfternoon" runat="server" />
                <label class="form-check-label" for="chkAfternoon">Afternoon</label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="checkbox" id="chkEvening" runat="server" />
                <label class="form-check-label" for="chkEvening">Evening</label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="checkbox" id="chkNight" runat="server" />
                <label class="form-check-label" for="chkNight">Night</label>
            </div>
        </div>

        <!-- Additional Doctor's Notes -->
        <div class="form-group mb-4">
            <label for="doctorNotes" class="form-label"><i class="fas fa-sticky-note me-2"></i>Doctor's Notes</label>
            <asp:TextBox ID="txtDoctorNotes" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Enter any additional notes" Rows="3"></asp:TextBox>
        </div>

        <!-- Action Buttons -->
        <div class="d-flex justify-content-between">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary px-4" OnClientClick="history.back(); return false;" />
            <asp:Button ID="btnSubmitPrescription" runat="server" Text="Submit Prescription" CssClass="btn btn-primary px-4" OnClick="SubmitPrescription" />
        </div>

        <!-- Confirmation Message -->
        <asp:Label ID="lblConfirmation" runat="server" CssClass="alert alert-success mt-4" Visible="false"></asp:Label>
    </div>
</div>

</asp:Content>


