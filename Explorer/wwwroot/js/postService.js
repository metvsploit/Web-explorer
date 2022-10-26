
export class PostService  {

    static async GetTreeNode() {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("/home/get", {
                method: "GET",
                headers: { "Accept": "application/json" }
            });
            
            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async RenameFile(model) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/file", {
                method: "PUT",
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                },
                body: JSON.stringify(model)
            });
            $("#loader").css("display", "none");
            const data = await response.json()
            return data;
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async DeleteFile(id) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/file/"+id, {
                method: "DELETE",
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                }
            });
            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async AddFile(model) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/file", {
                method: "POST",
                headers: { 'Content-type': 'application/json; charset=UTF-8' },
                body: JSON.stringify(model)
            });

            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async RenameFolder(model) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/folder", {
                method: "PUT",
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                },
                body: JSON.stringify(model)
            });
            $("#loader").css("display", "none");
            const data = await response.json()
            return data;
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async DeleteFolder(id) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/folder/" + id, {
                method: "DELETE",
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                }
            });
            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async AddFolder(model) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/folder/", {
                method: "POST",
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                },
                body: JSON.stringify(model)
            });
            $("#loader").css("display", "none");
                return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async CreateProject(model) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/folder/project", {
                method: "POST",
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                },
                body: JSON.stringify(model)
            });
            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async GetTypes() {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("/api/type", {
                method: "GET",
                headers: { "Accept": "application/json" }
            });

            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async AddType(model) {
        try {
            console.log(model)
            $("#loader").css("display", "block");
            const response = await fetch("api/type/"+model.format, {
                method: "POST",

                body: model.formData
            });
            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }

    static async DeleteType(id) {
        try {
            $("#loader").css("display", "block");
            const response = await fetch("api/type/" + id, {
                method: "DELETE",
                headers: {
                    'Content-type': 'application/json; charset=UTF-8'
                }
            });
            $("#loader").css("display", "none");
            return await response.json();
        }
        catch (err) {
            $("#loader").css("display", "none");
            alert('Ошибка при выполнении запроса к базе данных ' + err);
        }
    }
}