'use strict';
var notesController = angular.module('notesController', []);

notesController.controller('notesController', [
    '$scope',
    function ($scope) {
        $scope.notes = "No notes for selected cell.";
    }
]);
//# sourceMappingURL=notesController.js.map
