if (true) {
    $("#facebook-inbox").show();
    fbInboxFillPage(
        "https://www.facebook.com/bizwebvietnam/?fref=ts",
        "/Content/Images/fb-icon-1.png",
        "#019adb",
        "#019adb",
        "#FFFFFF",
        "0",
        "Chat với ch&#250;ng t&#244;i"
    );
    facebookShowPanelButton();
}

function facebookShowPanelButton() {
    $(".facebook-inbox-tab").click(function () {
        var isHiden = $("#facebook-inbox").attr("hiden");
        fbInboxHideBottom(isHiden);
    });
}

function fbInboxHideBottom(isHiden) {
    if (isHiden !== undefined && isHiden !== "false") {
        $("#facebook-inbox").attr("hiden", false);
        $("#facebook-inbox-frame").hide();
    } else {
        $("#facebook-inbox-frame").show();
        $("#facebook-inbox").attr("hiden", true);
    }
}

function fbInboxFillPage(facebookPage, imageSrc, bgColor, bdColor, textColor, location, title) {
    if (facebookPage.indexOf("facebook.com/") <= -1) {
        facebookPage = "https://facebook.com/" + facebookPage;
    }
    $("#facebook-inbox-frame .fb-page").attr("data-href", facebookPage);
    $("#facebook-inbox-frame .fb-page blockquote").attr("cite", facebookPage);
    $("#facebook-inbox-frame .fb-page blockquote a").attr("href", facebookPage);
    if (imageSrc != "") {
        $(".facebook-inbox-tab-icon").html("<img src='https://facebookinbox.bizwebapps.vn/" + imageSrc + "' />");
    }
    $(".facebook-inbox-tab").css({ "background-color": bgColor, "border-color": bdColor, "color": textColor });
    $(".facebook-inbox-tab .facebook-inbox-tab-title").html(title);
    $(".facebook-inbox-tab .facebook-inbox-tab-title").css('font-weight', 'bold');
    if (location == "0") {
        $("#facebook-inbox").css("right", "35px");
    }
    else {
        $("#facebook-inbox").css("left", "35px");
    }
}