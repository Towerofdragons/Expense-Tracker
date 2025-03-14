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
});

//Handling overlay and pop up modal forms 
$(document).on("click", ".edit-expense-button", function (e) {
    e.preventDefault();
    // Fetch the Edit Expense Form Partial View
    $.ajax({
        url: "/Dashboard/EditExpenseForm",
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
    // Fetch the Edit Expense Form Partial View
    $.ajax({
        url: "/Dashboard/EditIncomeForm",
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


   