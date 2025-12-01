<?php

namespace App\Http\Controllers;

use App\Models\Task;
use Illuminate\Http\Request;

class TaskController extends Controller
{
    // Show all tasks
    public function index()
    {
        $tasks = Task::all();
        return view('tasks', compact('tasks'));
    }

    // Add new task
    public function store(Request $request)
    {
        $request->validate([
            'name' => 'required|string|max:255',
        ]);

        Task::create(['name' => $request->name]);

        return redirect('/');
    }

    // Delete task
    public function destroy($id)
    {
        Task::destroy($id);
        return redirect('/');
    }
}
