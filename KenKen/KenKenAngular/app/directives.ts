'use strict';

var kenkenDirectives = angular.module('kenkenDirectives', []);

kenkenDirectives.directive('ngFocusWhenSelected', $timeout=> (scope, element)=> {
    // 1 ms timeout required or the text within is not selected
    $timeout(()=> {
        element[0].select();
    }, 1);
});
kenkenDirectives.directive('ngEnter', ()=> (scope, element, attrs)=> {
    element.bind("keydown keypress", event=> {
        if (event.which === 13) {
            scope.$apply(()=> {
                scope.$eval(attrs.ngEnter);
            });

            event.preventDefault();
        }
    });
});