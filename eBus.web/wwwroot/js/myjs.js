function myFunction() {

    var input, filter, ul, li, a, i, txtValue, showDiv;
    //showDiv = document.getElementById("prikaziListe");
    //showDiv.style.display = "block";

    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    ul = document.getElementById("myUL");
    ul.style.display = "block";
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

function prikaziPolazista() {
    var input, filter, ul, li, a, i, txtValue, showDiv;
    //showDiv = document.getElementById("prikaziListe");
    //showDiv.style.display = "block";

    input = document.getElementById("polaziste");
    filter = input.value.toUpperCase();
    ul = document.getElementById("polazisteUL");
    ul.style.display = "block";
    li = ul.getElementsByTagName("li");

    for (var i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];

        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

function izaberiOdrediste(naziv) {

    var input;

    input = document.getElementById("myInput");
    input.value = naziv;
}

function izaberiPolaziste(naziv) {
    var input;
    input = document.getElementById("polaziste");
    input.value = naziv;
}

function reverse() {

    var inputP, inputO, zamjena;

    inputP = document.getElementById("polaziste");
    inputO = document.getElementById("myInput");

    zamjena = inputP.value;
    inputP.value = inputO.value;
    inputO.value = zamjena;

    prikaziPolazista();
    myFunction();
  
}
function closeDetails(){

    var divC;
    divC = document.getElementById("detailDiv");

    divC.style.display = "none";

    

   
}

(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

$("#searchRezervacija").on("keyup", function () {
    var value = $(this).val().toLowerCase();
    $("#searchRow .row").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});


$(".btnC").change(function () {

    var id = $(this).attr('data-recordid');

    var urlzapoziv = "/Notifikacije/OznaciNeprocitanim?notifId=" + id;

    $.ajax({
        type: "POST",
        url: urlzapoziv,
        success: function (data) {

        }

    });

});

$('[data-toggle="tooltip"]').tooltip();


