var fileOrder = 2;

function addFile() 
{
    var ele = document.getElementById("fileInput");

    var inputFile = document.createElement('input');
    var tableRow = document.createElement('tr');
    var tableDataCellFirst = document.createElement('td');
    var tableDataCellSecond = document.createElement('td');
    var textField = document.createElement('textarea');

    inputFile.setAttribute('type', 'file');
    inputFile.setAttribute('name', 'picture' + fileOrder);
    inputFile.setAttribute('id', 'picture' + fileOrder);
    inputFile.setAttribute('onchange', 'addFile()');

    textField.setAttribute('rows', '4');
    textField.setAttribute('cols', '20');
    textField.setAttribute('id', 'description' + fileOrder);
    textField.setAttribute('name', 'description' + fileOrder);

    tableDataCellFirst.appendChild(inputFile);
    tableDataCellSecond.appendChild(textField);
    tableRow.appendChild(tableDataCellFirst);
    tableRow.appendChild(tableDataCellSecond);

    ele.appendChild(tableRow);

    fileOrder++;
};

function clearTextLink() {
    var ele = document.getElementById("newLink");
};