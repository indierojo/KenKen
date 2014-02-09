'use strict';

var notesController = angular.module('notesController', []); 

notesController.controller('notesController', [
    '$scope',
    ($scope: NotesScope) => {
        $scope.notes = "No notes for selected cell.";
    }
]); 