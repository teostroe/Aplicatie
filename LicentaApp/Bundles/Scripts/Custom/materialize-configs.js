$(document).ready(function () {
    $('select').formSelect();
    $('.sidenav').sidenav();
    $('.tooltipped').tooltip();
    $('.tabs').tabs();
    $('.modal').modal();
    initCalendar();

});

function initCalendar() {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        //defaultDate: "1990/01/01",
        //setDefaultDate: true,
        yearRange: [1900, (new Date()).getFullYear()],
        i18n: {
            cancel: "Anuleaza",
            months: [
                "Ianuarie",
                "Februarie",
                "Martie",
                "Aprilie",
                "Mai",
                "Iunie",
                "Iulie",
                "August",
                "Septembrie",
                "Octombrie",
                "Noiembrie",
                "Decembrie"
            ],
            monthsShort: [
                "Ian",
                "Feb",
                "Mar",
                "Apr",
                "Mai",
                "Iun",
                "Iul",
                "Aug",
                "Sep",
                "Oct",
                "Noi",
                "Dec"
            ],
            weekdays: [
                "Luni",
                "Marti",
                "Miercuri",
                "Joi",
                "Vineri",
                "Sambata",
                "Duminica"
            ],
            weekdaysShort: [
                "Lun",
                "Mar",
                "Mie",
                "Joi",
                "Vin",
                "Sam",
                "Dum"
            ],
            weekdaysAbbrev: [
                "L", "M", "M", "J", "V", "S", "D"
            ]

        }
    });
}