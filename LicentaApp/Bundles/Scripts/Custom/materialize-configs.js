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
        defaultDate: "01/01/1990",
        setDefaultDate: true,
        i18n: {
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