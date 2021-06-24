$(function () {
    $('.OnlyLatin').on('keyup', function () {
        OnlyLatin(this);
    });
    $('.OnlyLatin').on('input', function () {
        OnlyLatin(this);
    });
    function OnlyLatin(elem)
    {
        var inputLat = elem;       
        inputLat.value = inputLat.value.match(/[a-z0-9_\-.]+/g);       
    }
});