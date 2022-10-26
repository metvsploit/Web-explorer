import { nodes } from './states.js';
import { PostService } from './postService.js';
import { model } from './states.js';
import { folderModel } from './states.js';
import { fileModel } from './states.js';
import { typeModel } from './states.js';

document.oncontextmenu = function () { return false; };

$("#tree-parent").on('mousedown', '.sp', function (e) {
    const id = $(this).attr('id');
    model.id = id.split('_')[1];
    $(this).css('font-weight', 'bold');
    if ($('.context-menu').length) {
        setTimeout(() => { $('.context-menu').remove(); }, 10);
        $(this).css('font-weight', 'normal');
    }
    if (event.which === 3) {
        $('<div/>', {
            class: 'context-menu'
        })
            .css({
                left: event.pageX + 'px',
                top: event.pageY + 'px'
            })
            .appendTo('body')
            .append(
                $('<ul/>').append(`<li><span class = "rename-file_${model.id}" id = "rename-file">Переименовать</span></li>`)
                    .append(`<li><span class="download-file" id="download-file_${model.id}" >Скачать</span></li>`)
                    .append(`<li><span class="delete-file" id="delete-file_${model.id}">Удалить</span></li>`)
            )
            .show('fast');
    }
    $(document).click(function () {
        setTimeout(() => { $('.context-menu').remove(); }, 10);
        $(`#${id}`).css('font-weight', 'normal');
    });
});


/*context-menu*/
$("#tree-parent").on('mousedown', '.sf', function (e) {
    const id = $(this).attr('id');
    const split = id.split('_');
    model.name = split[2];
    model.id = split[1];
    folderModel.parentId = split[2];
    console.log(model.name, model.id, folderModel.parentId)
    $(this).css('font-weight', 'bold');
    if ($('.context-menu').length) {
        setTimeout(() => { $('.context-menu').remove(); }, 10);
        $(this).css('font-weight', 'normal');
    }
    if (event.which === 3) {
        $('<div/>', {
            class: 'context-menu'
        })
            .css({
                left: event.pageX + 'px',
                top: event.pageY + 'px'
            })
            .appendTo('body')
            .append(
                $('<ul/>').append(`<li><span class="add-folder" id="add-folder_${model.id}_${folderModel.parentId}">Добавить папку</span></li>`)
                    .append(`<li><span class="load-file" id="load-file_${model.id}">Добавить файл</span></li>`)
                    .append(`<li><span class="rename-folder" id="rename-folder_${model.id}_${folderModel.parentId}">Переименовать</span></li>`)
                    .append(`<li><span class="delete-folder" id="delete-folder_${model.id}_${folderModel.parentId}">Удалить</span></li>`)
            )
            .show('fast');
    }
    $(document).click(function () {
        setTimeout(() => { $('.context-menu').remove(); }, 10);
        $(`#${id}`).css('font-weight', 'normal');
    });
});


/*RenameFile*/
$("body").on('click', '#rename-file', function (e) {
    
    const file = nodes.files.find(x => x.fileId == model.id);
    $('#filename-input').val('');
    $('#exampleModalLabel').html(`${file.fileName}.${file.type.format}`);
    $('#file-message').html('');
    $('#modal-file').find('.modal').modal('show');
});

$('#new-filename-submit').click(async function () {
    model.name = $('#filename-input').val();
    const file = nodes.files.find(x => x.fileId == model.id);
    console.log("file",file)
    const response = await PostService.RenameFile(model);
    if (response.success) {
        console.log('success');
        const index = nodes.files.findIndex(x => x.fileId == model.id);
        nodes.files[index].fileName = model.name;
        $('#file-message').html('Файл успешно переименован');
        $("#tree-parent").find(`#file_${model.id}`).html(`${model.name}.${file.type.format}`);
        $('#pills-tab').find(`#pills-${model.id}-tab`).html(`${model.name}.${file.type.format} <span id="close_${model.id}" class="close">x</span>`);
    }
    else {
        $('#file-message').html('Не удалось переименовать файл: ' + response.message);
    }
});

/*DownloadFile*/
$("body").on('click', '.download-file', async function (e) {

    const file = nodes.files.find(x => x.fileId == model.id);
    var downloadFile = new Blob([file.content], { type: "text/plain;charset=utf-8" });
    if (window.navigator.msSaveOrOpenBlob) // IE10+
        window.navigator.msSaveOrOpenBlob(downloadFile, filename);
    else {
        var a = document.createElement("a"),
            url = URL.createObjectURL(downloadFile);
        a.href = url;
        a.download = file.fileName + '.' + file.type.format;
        document.body.appendChild(a);
        a.click();
        setTimeout(function () {
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);
        }, 0);
    }
});


/*DeleteFile*/
$("body").on('click', '.delete-file', async function (e) {
  
    const index = nodes.files.findIndex(x => x.fileId == model.id);
    const response = await PostService.DeleteFile(model.id);
    if (response.success) {
        nodes.files.splice(index, 1);
        $("#tree-parent").find(`#li-file_${model.id}`).remove();
        $(`#pills-${model.id}-tab`).remove();
        $(`#pills-${model.id}`).remove();
    }
    else {
        alert("Ошибка запроса: " + response.message);
    }
});

/*AddFile*/
$("body").on('click', '.load-file', async function (e) {

    const response = await PostService.GetTypes();
    if (response.success) {
        for (const type of response.data) {
            $('#type-selector').append($('<option>', {
                id: type.format,
                value: type.typeId,
                text: type.format
            }));
        }

        $('#file-load-input').val('');
        $('#file-load-message').html('');
        $('#modal-file-load').find('.modal').modal('show');
    }
    else {
        alert('Ошибка запроса: ' + response.message);
    }
    fileModel.folderId = model.id;
});

$('#formFile').change(function (ev) {
    console.log('change')
    var file = document.getElementById("formFile").files[0];
    if (file) {
        var reader = new FileReader();
        var temp = file.name.split('.');
        fileModel.fileName = temp[temp.length - 2];
        var type = temp[temp.length - 1];
        globalType = type;
        var selector = $('select').find(`#${type}`);
        if (selector.length) {
            fileModel.typeId = $(`#${type}`).val();
            selector.prop('selected', true);
        }
        else {
            console.log(type)
            $('#type-selector').append($('<option>', {
                text: type + ' (не добавлен)',
                selected: true,
                value: 0,
                id: type
            }));
            fileModel.typeId = 0;
        }
        
        reader.readAsText(file, "UTF-8");
        reader.onload = function (evt) {
            fileModel.content = evt.target.result;
        }
        reader.onerror = function (evt) {
            alert("Не удалось прочитать файл");
            $('#modal-file-load').find('.modal').modal('hide');
        }
        $('#new-type-submit').prop("disabled", false);
        $('#delete-type-submit').prop("disabled", false);
    }
});


$('#new-file-submit').click(async function () {
    fileModel.description = $('#file-description').val();
    const response = await PostService.AddFile(fileModel);
    if (response.success) {
        nodes.files.push(response.data);
        console.log(nodes.files)
        $("#tree-parent").find(`#${model.id}`)
            .append(`<li id="li-file_${response.data.fileId}"><img class="file-img" src="${response.data.type.icon}"><span title="${response.data.description}"><span id="file_${response.data.fileId}" class="sp">${response.data.fileName}.${response.data.type.format}</span>ㅤ</span></li>`);
        $('#modal-file-load').find('.modal').modal('hide');
    }
    else {
        $('#file-load-message').html('Возникла ошибка при загрузке файла');
    }
    
    $('#new-type-submit').prop("disabled", true);
    $('#delete-type-submit').prop("disabled", true);
    $('#fileForm').replaceWith($('#fileForm').clone());
});


/*AddFormat*/
$('#formType').change(async function (ev) {
    var file = document.getElementById("formType").files[0];
    console.log(file);
    if (file) {

        const formData = new FormData();
        formData.append("Icon", file);
        typeModel.formData = formData;
        typeModel.format = globalType;
        const response = await PostService.AddType(typeModel);
        if (response.success) {
            $('#file-load-message').html('Тип добавлен');
            var selector = $(`select`).find(`#${globalType}`);
            selector.attr("id", response.data);
            selector.html(`${globalType}`);
            fileModel.typeId = response.data;
        }
        else {
            $('#file-load-message').html(response.message);
        }
    }
    else {
        alert('Не удалось загрузить иконку');
    }
})


/*DeleteFormat*/
$("#delete-type-submit").click(async function (ev) {
    const response = await PostService.DeleteType(fileModel.typeId);
    if (response.success) {
        $(`select`).find(`#${globalType}`).html(`${globalType} (не добавлен)`);
        $('#file-load-message').html(response.message);
    }
    else {
        $('#file-load-message').html(response.message);
    }
})

/*RenameFolder*/
$("body").on('click', '.rename-folder', async function (e) {
    $('#folder-message').html('');
    $('#folder-input').val('')
    $('#modal-folder').find('.modal').modal('show');
});

$('#new-foldername-submit').click(async function () {
    model.name = $('#folder-input').val();
    const response = await PostService.RenameFolder(model);
    if (response.success) {
        $('#folder-message').html('Папка успешно переименована');
        $("#tree-parent").find(`#folder_${model.id}_${folderModel.parentId}`).html(`${model.name}`);
        }
    else {
        $('#file-message').html('Не удалось переименовать папку');
    }
});

/*DeleteFolder*/
$("body").on('click', '.delete-folder', async function (e) {
    const response = await PostService.DeleteFolder(model.id);
    if (response.success) {
        if (folderModel.parentId == 0) {
            $("#tree-parent").find(`#project${model.id}`).remove();
        }
        else {
            $("#tree-parent").find(`#pfolder_${model.id}_${folderModel.parentId}`).remove();
        }

    }
    else {
        alert("Ошибка запроса: " + response.message);
    }
});

/*AddFolder*/
$("body").on('click', '.add-folder', async function (e) {
    $('#folder-message').html('');
    $('#folder-input').val('')
    $('#modal-folder').find('.modal').modal('show');
});

$('#new-folder-submit').click(async function () {
    folderModel.name = $('#folder-input').val();
    const id = folderModel.parentId;
    folderModel.parentId = model.id;
    const response = await PostService.AddFolder(folderModel);

    if (response.success) {
        $('#folder-message').html("Папка успешно добавлена");
        $("#tree-parent").find(`#${model.id}`)
            .append(`<li id="pfolder_${response.data}_${model.id}"><span id="folder_${response.data}_${model.id}" class="sf">${folderModel.name}</span><div class="drop dropM">🗀</div>ㅤ<ul id="${response.data}"></ul></li>`);
        }
    else {
        $('#folder-message').html(response.message);
    }
    $('#modal-folder').find('.modal').modal('hide');
});

/*CreateFolder*/
$('#create-project-btn').click(async function () {
    folderModel.name = $('#project-name').val();
    folderModel.parentId = 0;
    const response = await PostService.CreateProject(folderModel);
    if (response.success) {
        $("#tree-parent")
                .append(`<ul id="project${response.data}"><li id="pfolder_${response.data}_0"><span id="folder_${response.data}_0" class="sf">${folderModel.name}</span><div class="drop dropM">🗀</div>ㅤ<ul id="${response.data}"></ul></li></ul>`);
    }
    else {
        alert(response.message);
    }
});

let globalType = '';