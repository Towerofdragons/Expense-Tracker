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
$(document).on("click", ".edit-button", function (e) {
    e.preventDefault();
    // Show overlay and modal
    $(".overlay").fadeIn();
    $(".editModal").fadeIn();
});
//$(document).ready(function () {
//    $(".edit-button").on("click", function (e) {
//        console.log("clicked");
//        e.preventDefault();
//        // Show overlay and modal
//        $(".overlay").fadeIn();
//        $(".editModal").fadeIn();
//    });
//});


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


   