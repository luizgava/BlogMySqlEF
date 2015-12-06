$(document).ready(function () {
    var Tags = new Array();

    $('#adicionar').on('click', function () {
        var tag = $('#Tag').val();
        var existe = Tags.filter(function (v) {
            return v.Tag.toLowerCase() === tag.toLowerCase();
        })[0];

        if (existe != undefined) {
            alert('Esta TAG já foi informada.');
            return;
        }

        Tags.push(new Object({ Tag: tag }));
        montaListaPeloArray();

        $('#Tag').val('');
        $('#Tag').focus();
    });

    $('body').on('click', '.remover-tag', function () {
        var tag = $(this).attr('tag');

        Tags = $.grep(Tags, function (e) {
            return e.Tag != tag;
        });

        montaListaPeloArray();
    });

    function montaListaPeloArray() {
        var form = $('form');

        $('input[Name="Tags"]').remove();
        $("#resultado").empty();
        $(Tags).each(function () {
            $("#resultado").append('<li><span>' + this.Tag + '</span><a tag="' + this.Tag + '" class="remover-tag icone-excluir" title="Remover" > </a></li>');
            form.append('<input type="hidden" name="Tags" value="' + this.Tag + '">');
        });
    }

    $("input[Name='Tags']")
        .map(function () {
            var tag = $(this).val();
            Tags.push(new Object({ Tag: tag }));
        }).get();
});