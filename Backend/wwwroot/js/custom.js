//Handle tab switching
$(document).ready(function(){

    $("#income-form, #income-table").show();
    $("#expense-form, #expense-table").hide();

    $('#income-tab').click(function(){
        $("#income-form, #income-table").show();
        $("#expense-form, #expense-table").hide();
        $(this).addClass("active");
        $("#expense-tab").removeClass("active");
    });

    $('#expense-tab').click(function(){
        $("#income-form, #income-table").hide();
        $("#expense-form, #expense-table").show();
        $(this).addClass("active");
        $("#income-tab").removeClass("active");
    });


    function reloadCharts() {
        $.get("/Dashboard/GetChartData", function(response) {
            // Update Pie Chart
            pieChart.options.data[0].dataPoints = response.pieData;
            pieChart.render();

            // Update Bar Chart
            barChart.options.data[0].dataPoints = response.barData;
            barChart.render();
        });
    }

    function reloadTables() {
        $.ajax({
            url: "/Dashboard/IncomeTable",
            type: "GET",
            cache: false,
            success: function (data) {
                $("#income-table").html(data); // Replace table content
            }
        });

        $.ajax({
            url: "/Dashboard/ExpenseTable",
            type: "GET",
            cache: false,
            success: function (data) {
                $("#expense-table").html(data); // Replace table content
            }
        });

        reloadCharts();
    }


    // Handle form submission for income
    $("#income-form").on("submit", function(e) {
        e.preventDefault();
        var formData = $(this).serialize();

        $.post("/Income/Add", formData, function(response) {
            reloadTables(); // Refresh tables after adding income
            $("#income-form")[0].reset(); // Reset form
        });
    });

    // Handle form submission for expense
    $("#expense-form").on("submit", function(e) {
        e.preventDefault();
        var formData = $(this).serialize();

        $.post("/Expense/Add", formData, function(response) {
            reloadTables(); // Refresh tables after adding expense
            $("#expense-form")[0].reset(); // Reset form
        });
    });

    
    // Delete income
    $(document).on("click", ".delete-income", function () {
        var incomeId = $(this).data("id");

        if (confirm("Are you sure you want to delete this income?")) {
            $.post("/Income/Delete", { id: incomeId }, function (response) {
                
                    reloadTables(); // Refresh table after deletion
                
            }).fail(function (xhr) {
                alert("Failed to delete income. Please try again.");
            });
        }
    });

    // Delete expense
    $(document).on("click", ".delete-expense", function () {
        var expenseId = $(this).data("id");

        if (confirm("Are you sure you want to delete this expense?")) {
            $.post("/Expense/Delete", { id: expenseId }, function (response) {
                
            reloadTables(); // Refresh table after deletion
                
            }).fail(function (xhr) {
                alert("Failed to delete expense. Please try again.");
            });
        }
    });
});




//Handling overlay and pop up modal forms 
$(document).on("click", ".edit-expense-button", function (e) {
    e.preventDefault();

    var expenseId = $(this).data("id");

    // Fetch the Edit Expense Form Partial View
    $.ajax({
        url: "/Dashboard/EditExpenseForm?Id=" + expenseId,
        type: "GET",
        success: function (data) {//request is successful, this function executes.
            $("#editExpenseContainer").html(data);//adds the modal and overlay in the container
            // Show overlay and modal
            $(".overlay").fadeIn();
            $(".edit-modal").fadeIn();
        },
        error: function () {
            alert("Failed to load edit form.");
        }
    });
    
});

$(document).on("click", ".cancel-button, .overlay ", function () {
    $(".overlay").fadeOut();
    $(".edit-modal").fadeOut();
})

$(document).on("click", ".edit-income-button", function (e) {
    e.preventDefault();

    var incomeId = $(this).data("id");

    // Fetch the Edit Expense Form Partial View
    $.ajax({
        url: "/Dashboard/EditIncomeForm?Id=" + incomeId,
        type: "GET",
        success: function (data) {//request is successful, this function executes.
            $("#editIncomeContainer").html(data);//adds the modal and overlay in the container
            // Show overlay and modal
            $(".overlay").fadeIn();
            $(".edit-income-modal").fadeIn();
        },
        error: function () {
            alert("Failed to load edit form.");
        }
    });

});


// Handle form submission for editing income
$(document).on("submit", "#edit-income-form", function (e) {
    e.preventDefault();

    var formData = $(this).serialize();

    $.ajax({
        url: "/Income/Edit",
        type: "POST",
        data: formData,
        success: function (response) {
            alert("Income updated successfully!");
            location.reload(); // Refresh page or update UI dynamically
        },
        error: function () {
            alert("Failed to update income.");
        }
    });
});




$(document).on("click", ".cancel-button, .overlay ", function () {
    $(".overlay").fadeOut();
    $(".edit-income-modal").fadeOut();
});

$(document).on("click", ".btn btn-warning btn-sm", function (e) {
    console.log("Clicked");
    e.preventDefault();
    // Fetch the Edit Expense Form Partial View
    $.ajax({
        url: "/Dashboard/EditUserForm",
        type: "GET",
        success: function (data) {//request is successful, this function executes.
            $("#editIncomeContainer").html(data);//adds the modal and overlay in the container
            // Show overlay and modal
            $(".overlay").fadeIn();
            $(".edit-users-modal").fadeIn();
        },
        error: function () {
            alert("Failed to load edit form.");
        }
    });

});

$(document).on("click", ".cancel-button, .overlay ", function () {
    $(".overlay").fadeOut();
    $(".edit-users-modal").fadeOut();
});



//Handling charts 
function renderCharts(totalIncome, totalExpense) {

    // Pie Chart Data
    var pieChart = new CanvasJS.Chart("pie-container", {
        animationEnabled: true,
        theme: "light2",
        data: [{
            type: "pie",
            indexLabel: "{label}: {y}%",
            indexLabelFontSize: 16,
            dataPoints: [
                {label: "Rent", y: 20},
                {label: "Groceries", y: 30},
                {label: "School Fees", y: 25 },
                {label: "Energy", y: 25}
            ]   
        }]
    });

    // Bar Chart Data
    var barChart = new CanvasJS.Chart("bar-container", {
        animationEnabled: true,
        theme: "light2",
        axisY: {
            title: "Amount",
            gridThickness: 1,
            labelFontSize: 14
        },
        data: [{
            type: "column",
            indexLabel: "{y}",
            indexLabelFontSize: 16,
            color: "#8A7DFF",
            dataPoints: [
                {label: "Income", y: totalIncome },
                {label: "Expense", y: totalExpense},
                {label: "Balance", y: totalIncome - totalExpense}
            ]
        }]
    });

    // Render Both Charts
    pieChart.render();
    barChart.render();
}

// Get values from the model and render charts
var totalIncome = parseFloat($("#totalIncome").val());
var totalExpense = parseFloat($("#totalExpense").val());
renderCharts(totalIncome, totalExpense);


   