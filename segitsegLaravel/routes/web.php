<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\TaskController;

// All routes on the same page
Route::get('/', [TaskController::class, 'index'])->name('tasks.index');
Route::post('/tasks', [TaskController::class, 'store'])->name('tasks.store');
Route::delete('/tasks/{id}', [TaskController::class, 'destroy'])->name('tasks.destroy');