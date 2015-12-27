//首页 功能点击
$(function () {
    $(".xtglclass li a").click(function () {
        var name = $(this).text();
        var url = $(this).attr("class");
        //alert(name);
        //alert(url);

        addTab(name, url);
    })
})

//GNQ 点击
$(function () {
    $(".GNQIndex li a").click(function () {
        var name = $(this).text();
        var url = $(this).attr("class");
        //alert(name);
        //alert(url);

       // AddTabPage(name, url);
    })
})
//增加tabs
function addTab(text, url) {
    if ($('#tt').tabs('exists', text)) {
        $('#tt').tabs('select', text);
    }
    else {
        $('#tt').tabs('add', {
            title: text,
            content: '<iframe scrolling="yes" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>',
            //href: url,
            dataType: 'html',
            closable: true,
            closed: true
        })
    }
}
function AddTabPage(title, url) {
    var jq = top.jQuery;

    if (jq("#tt").tabs('exists', title)) {
        jq("#tt").tabs('select', title);
    } else {
        var content = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
        jq("#tt").tabs('add', {
            title: title,
            content: content,
            closable: true
        });
    }
}