// Write your Javascript code.
function Department(id ,name) {
    var self = this;
    self.id = id;
    self.name = ko.observable(name);
}

var DeptModel = function (depts) {
    var self = this;
    self.Departments = ko.observableArray(depts);

    self.addDepartment = function () {
        self.Departments.push({
            name: "",
            id: 0
        });
    };

    self.removeDepartment = function (Department) {

        $.ajax({
            type: "DELETE",
            url: ("http://localhost:10441/api/dep/" + Department.id),
            datatype: "text/plain",
            contentType: "application/json; charset=utf-8"
        })
            .done(function (data) {
                if (data.item1) {
                    self.Departments.remove(Department);
                    window.alert('deleted');
                } else {
                    window.alert('error in server');
                }
            })
            .fail(function () { alert("error on connection"); });
       
    };

    self.save = function (Department) {
        alert("Could now transmit to server: " + ko.utils.stringifyJson(Department));
        // ko.utils.postJson($("form")[0], self.Departments);
        $.ajax({
                type: "POST",
                url: "http://localhost:10441/api/dep",
                data: ko.utils.stringifyJson(Department),
                datatype: "text/plain",
                contentType: "application/json; charset=utf-8"
            })
            .done(function (data) {
                if (data.item1) {
                    Department.id = data.item2.id;
                } else {
                    window.alert('error in server');
                }
            })
            .fail(function () { alert("error on connection"); });
    };
};
