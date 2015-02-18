

$('.submitOnEnter').keydown(function (event) {
    if (event.keyCode == 13) {
        this.form.submit();
        return false;
    }
});

function clickSiblingSubmit(link) {
     var me = $(link);
    me.siblings('input[type="submit"]').click();
}