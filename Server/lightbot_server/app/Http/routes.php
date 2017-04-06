<?php

/*
|--------------------------------------------------------------------------
| Application Routes
|--------------------------------------------------------------------------
|
| Here is where you can register all of the routes for an application.
| It's a breeze. Simply tell Laravel the URIs it should respond to
| and give it the controller to call when that URI is requested.
|
*/

Route::get('/', function () {
    return view('welcome');
});

Route::get('score/cleardb', 'ScoreController@ResetBD');
Route::get('score/list', 'ScoreController@GetList');
Route::post('score/add', 'ScoreController@AddScore');

