//school-index.js
var module = angular.module('SchoolApp', ['ngRoute']);
module.config(function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "classController",
        templateUrl: "/templates/classesView.html"
    });
    $routeProvider.when("/addClass", {
        controller: "editClassController",
        templateUrl: "/templates/editClassView.html"
    });
    $routeProvider.when("/addStudentClass", {
        controller: "editStudentClassController",
        templateUrl: "/templates/editClassDetailsView.html"
    });
    $routeProvider.when("/class/:id", {
        controller: "editClassController",
        templateUrl: "/templates/editClassView.html"
    });
    $routeProvider.when("/studentClass/:id", {
        controller: "editStudentClassController",
        templateUrl: "/templates/editClassDetailsView.html"
    });
    $routeProvider.when("/addStudent", {
        controller: "addStudentController",
        templateUrl: "/templates/addStudentView.html"
    });
    $routeProvider.otherwise({redirectTo:"/"});
});

module.factory("dataService", ["$http", "$q", function ($http, $q) {

    var _classes = [];
    var _classDetails = [];
    var _selectedClass = null;

    var _students = [];
    var _freeStudents = [];

    var _isInit = false;

    var _isReady = function () {
        return _isInit;
    }

    var _getClasses = function () {

        var deferred = $q.defer();

        $http.get("/api/v1/classes")
          .then(function (result) {
              // Successful
              angular.copy(result.data, _classes);
              _isInit = true;
              deferred.resolve();
          },
          function () {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    };
    
    function _findClass(id) {
        var found = null;

        $.each(_classes, function (i, item) {
            if (item.id == id) {
                found = item;
                return false;
            }
        });

        return found;
    }

    var _getClassById = function (id) {
        var deferred = $q.defer();

        if (_isReady()) {
            var oldClass = _findClass(id);
            if (oldClass) {
                deferred.resolve(oldClass);
            } else {
                deferred.reject();
            }
        } else {
            _getClasses()
              .then(function () {
                  // success
                  var oldClass = _findClass(id);
                  if (oldClass) {
                      deferred.resolve(oldClass);
                  } else {
                      deferred.reject();
                  }
              },
              function () {
                  // error
                  deferred.reject();
              });
        }

        return deferred.promise;
    };

    var _updateClass = function (updatedClass) {
        var deferred = $q.defer();

        $http.put("/api/v1/classes", updatedClass)
         .then(function (result) {
             // success
             var newlyUpdatedClass = result.data;
             //_isInit = false;
             deferred.resolve(newlyUpdatedClass);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteClass = function (classToDelete) {
        if (confirm("Are you sure you want to delete this class?")) {
            var deferred = $q.defer();

            $http.delete("/api/v1/classes?classId=" + classToDelete.id)
             .then(function (result) {
                 // success
                 _getClasses();
                 deferred.resolve();
             },
             function () {
                 // error
                 deferred.reject();
             });

            return deferred.promise;
        }
    };

    var _addClass = function (updatedClass) {
        var deferred = $q.defer();

        $http.post("/api/v1/classes", updatedClass)
         .then(function (result) {
             // success
             var newlyUpdatedClass = result.data;
             _isInit = false;
             deferred.resolve(newlyUpdatedClass);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _addStudent = function (newStudent) {
        var deferred = $q.defer();

        $http.post("/api/v1/students", newStudent)
         .then(function (result) {
             // success
             var newAddedStudent = result.data;
             _isInit = false;
             deferred.resolve(newAddedStudent);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _updateStudentClass = function (updatedStudentClass) {
        var deferred = $q.defer();

        $http.put("/api/v1/studentclass", updatedStudentClass)
         .then(function (result) {
             // success
             var newlyUpdatedStudentClass = result.data;
             //_isInit = false;
             deferred.resolve(newlyUpdatedStudentClass);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _deleteStudentClass = function (stuentClassToDelete) {
        if (confirm("Are you sure you want to delete this student?")) {
            var deferred = $q.defer();

            $http.delete("/api/v1/studentclass?studentClassId=" + stuentClassToDelete.id)
             .then(function (result) {
                 // success
                 _getClassDetails(_selectedClass);
                 deferred.resolve();
             },
             function () {
                 // error
                 deferred.reject();
             });

            return deferred.promise;
        }
    };

    var _addStudentClass = function (updatedStudentClass) {
        var deferred = $q.defer();

        $http.post("/api/v1/studentclass", updatedStudentClass)
         .then(function (result) {
             // success
             var newlyUpdatedStudentClass = result.data;
             deferred.resolve();
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };
    
    var _getClassDetails = function (_currentClass) {
        _selectedClass = _currentClass;
        var deferred = $q.defer();

        $http.get("/api/v1/classes/" + _selectedClass.id + "/details")
          .then(function (result) {
              // Successful
              angular.copy(result.data, _classDetails);
              _isInit = true;
              deferred.resolve();
          },
          function () {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    };

    var _getStudentClassById = function (id) {
        var deferred = $q.defer();

        $http.get("/api/v1/studentclass?id=" + id)
          .then(function (result) {
              // Successful
              var oldStudentClass = result.data;              
              deferred.resolve(oldStudentClass);
          },
          function () {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    };

    var _getStudents = function () {

        var deferred = $q.defer();

        $http.get("/api/v1/students")
          .then(function (result) {
              // Successful
              angular.copy(result.data, _students);
              deferred.resolve();
          },
          function () {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    };

    var _getFreeStudents = function (classId, studentId) {

        var deferred = $q.defer();

        $http.get("/api/v1/students?classId=" + classId + "&studentId=" + studentId)
          .then(function (result) {
              // Successful
              angular.copy(result.data, _freeStudents);
              deferred.resolve();
          },
          function () {
              // Error
              deferred.reject();
          });
        return deferred.promise;
    };

    return {
        classes: _classes,
        classDetails: _classDetails,
        selectedClass: _selectedClass,
        getClasses: _getClasses,
        getClassDetails: _getClassDetails,
        isReady: _isReady,
        getClassById: _getClassById,
        updateClass: _updateClass,
        addClass: _addClass,
        deleteClass: _deleteClass,
        students: _students,
        freeStudents: _freeStudents,
        getStudents: _getStudents,
        getFreeStudents: _getFreeStudents,
        getStudentClassById: _getStudentClassById,
        updateStudentClass: _updateStudentClass,
        addStudentClass: _addStudentClass,
        deleteStudentClass: _deleteStudentClass,
        addStudent: _addStudent
    };
}]);

module.controller('classController', ["$scope", "$http", "dataService", function ($scope, $http, dataService) {
    $scope.idSelectedClass = -1;

    $scope.data = dataService;
    $scope.isBusy = false;
    
    if (dataService.isReady() == false) {
        $scope.isBusy = true;

        dataService.getClasses()
          .then(function () {
              // success
          },
          function () {
              // error
              alert("could not load classes");
          })
          .then(function () {
              $scope.isBusy = false;
          });
    }

    $scope.setSelectedClass = function (currentClass) {
        if ($scope.idSelectedClass != currentClass.id) {
            $scope.idSelectedClass = currentClass.id;
            $scope.isBusy = true;
            $scope.data.selectedClass = currentClass;
            dataService.getClassDetails(currentClass)
                .then(function () {
                    //success
                },
                function () {
                    //error
                    alert("could not load class students");
                })
                .then(function () {
                    $scope.isBusy = false;
                });
        }
        else {
            $scope.idSelectedClass = -1;
            $scope.data.selectedClass = null;
        }
    };

}]);

module.controller('editClassController', ["$scope", "dataService", "$window", "$routeParams", function ($scope, dataService, $window, $routeParams) {
    $scope.updatedClass = null;
    if ($routeParams.id != null) {
        dataService.getClassById($routeParams.id)
          .then(function (oldClass) {
              // success
              $scope.updatedClass = oldClass;
          },
          function () {
              // error
              $window.location.href = "/";
          });
    }


      $scope.saveClass = function () {
          if ($scope.updatedClass.id != null && $scope.updatedClass.id > 0) {
              dataService.updateClass($scope.updatedClass)
                .then(function () {
                    // success
                    $window.location.href = "/";
                },
                function () {
                    // error
                    alert("could not update the class");
                });
          }
          else {
              $scope.updatedClass.id = 0;
              dataService.addClass($scope.updatedClass)
                .then(function () {
                    // success
                    $window.location.href = "/";
                },
                function () {
                    // error
                    alert("could not insert this class");
                });
          }

      };
  }]);

module.controller('editStudentClassController', ["$scope", "dataService", "$window", "$routeParams", function ($scope, dataService, $window, $routeParams) {
    $scope.updatedStudentClass = null;
    if ($routeParams.id != null) {
        dataService.getStudentClassById($routeParams.id)
          .then(function (oldStudentClass) {
              // success
              $scope.updatedStudentClass = oldStudentClass;
              dataService.getFreeStudents(oldStudentClass.classId, oldStudentClass.studentId);
          },
          function () {
              // error
              $window.location.href = "/";
          });
    }
    else {

        dataService.getFreeStudents(dataService.selectedClass.id, 0);
    }
    $scope.allowedStudents = dataService.freeStudents;

    $scope.saveStudentClass = function () {
        if ($scope.updatedStudentClass.id != null && $scope.updatedStudentClass.id > 0) {
            dataService.updateStudentClass($scope.updatedStudentClass)
              .then(function () {
                  // success
                  $window.location.href = "/";
              },
              function () {
                  // error
                  alert("could not update the class detail");
              });
        }
        else {
            $scope.updatedStudentClass.classId = dataService.selectedClass.id;
            dataService.addStudentClass($scope.updatedStudentClass)
              .then(function () {
                  // success
                  //$scope.isAllDone = true;
                  $window.location.href = "/";
              },
              function () {
                  // error
                  alert("could not insert this class detail");
              });
        }

    };
}]);

module.controller('addStudentController', ["$scope", "dataService", "$window", "$routeParams", function ($scope, dataService, $window, $routeParams) {
    
    $scope.saveStudent = function () {
        dataService.addStudent($scope.newStudent)
            .then(function () {
                // success
                $window.location.href = "/";
            },
            function () {
                // error
                alert("could not insert this student");
            });
    };
}]);
