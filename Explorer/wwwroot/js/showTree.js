export function ShowTree() {
    var ul = document.querySelectorAll('.treeline > li:not(:only-child) ul, .treeline ul ul');
    for (var i = 0; i < ul.length; i++) {
        var div = document.createElement('div');
        div.className = 'drop';
        div.innerHTML = '🗀';
        ul[i].parentNode.insertBefore(div, ul[i].previousSibling);
    }
}