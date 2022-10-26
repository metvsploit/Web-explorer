import { PostService } from './postService.js';
import { nodes } from './states.js';
import { ShowTree } from './showTree.js';

let DrawFlag = false;

$(document).ready(async function () {
    nodes.model = await PostService.GetTreeNode();
    if (nodes.model.success) {
        DrawTree();
        console.log(nodes.model)
    }
    else {
        alert(nodes.model.message);
    }
});


function DrawTree() {
    for (const folder of nodes.model.data) {
        if (folder.parentId == null)
            folder.parentId = 0;
        $("#tree-parent").append(`<ul id="project${folder.folderId}"><li id="pfolder_${folder.folderId}_${folder.parentId}"><span id="folder_${folder.folderId}_${folder.parentId}" class="sf">${folder.name}</span>ㅤ<ul id="${folder.folderId}"></ul></li></ul>`);
        DrawNode(folder);
    }
    ShowTree();
};

function DrawNode(folder, prev) {
    if (DrawFlag) {
        $(`#${prev}`)
            .append(`<li id="pfolder_${folder.folderId}_${folder.parentId}"><span id="folder_${folder.folderId}_${folder.parentId}" class="sf">${folder.name}</span>ㅤ<ul id="${folder.folderId}"></ul></li>`);
    }
    
    for (const f of folder.folders) {
        DrawFlag = true;
        DrawNode(f, `${folder.folderId}`);
    }
    if (folder.files.length > 0) {
        for (const file of folder.files) {
            if (file.type == null) {
                file.type = {
                    typeId: 0,
                    format: 'other',
                    icon: './images/unknown.png'
                };
                console.log(file.type.icon)
            }
            $(`#${folder.folderId}`)
                .append(`<li id="li-file_${file.fileId}"><img class="file-img" src="${file.type.icon}"/><span title="${file.description}"><span id="file_${file.fileId}" class="sp">${file.fileName}.${file.type.format}</span>ㅤ</li>`);
            nodes.files.push(file);
        }
    }
}


$("#tree-parent").on('click', '.drop', function (e) {
    this.innerHTML = (this.innerHTML == '🗀' ? '▼' : '🗀');
    this.className = (this.className == 'drop' ? 'drop dropM' : 'drop');
});


$("#tree-parent").on('click', '.sp', function (e) {
    const id = $(this).attr('id').split('_')[1];
    if (!document.getElementById(`pills-${id}`)) {
        const file = nodes.files.find(x => x.fileId == id);
        $('#pills-tab').append(`<li class="nav-item" role="presentation"><button class="nav-link" id="pills-${id}-tab" data-bs-toggle="pill" data-bs-target="#pills-${id}" type="button" role="tab" aria-controls="pills-${id}" aria-selected="true">${file.fileName}.${file.type.format} <span id="close_${file.fileId}" class="close">x</span></button></li>`);
        $('#pills-tabContent').append(`<div class="tab-pane fade show" id="pills-${id}" role="tabpanel" aria-labelledby="pills-${id}-tab">${file.content}</div>`);
    }
    $(`#pills-${id}-tab`).trigger('click');
})


$("#pills-tab").on('click', '.close', function (e) {
    const id = $(this).attr('id').split('_')[1];
    $(`#pills-${id}-tab`).remove();
    $(`#pills-${id}`).remove();
})

