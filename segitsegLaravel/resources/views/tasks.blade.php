<!DOCTYPE html>
<html>

<head>
    <title>Simple To-Do List</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            max-width: 600px;
            margin: 50px auto;
            padding: 20px;
        }

        h1 {
            text-align: center;
            color: #333;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }

        th,
        td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #f2f2f2;
        }

        tr:hover {
            background-color: #f5f5f5;
        }

        .delete-btn {
            background: #ff4444;
            color: white;
            border: none;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 3px;
        }

        .delete-btn:hover {
            background: #cc0000;
        }

        .add-form {
            margin-top: 20px;
        }

        input[type="text"] {
            width: 70%;
            padding: 10px;
            font-size: 16px;
        }

        button[type="submit"] {
            padding: 10px 20px;
            background: #4CAF50;
            color: white;
            border: none;
            cursor: pointer;
            font-size: 16px;
        }

        button[type="submit"]:hover {
            background: #45a049;
        }

        .no-tasks {
            text-align: center;
            color: #888;
            font-style: italic;
            margin: 20px 0;
        }
    </style>
</head>

<body>
    <h1>📝 Simple To-Do List</h1>

    @if(session('success'))
        <div style="background: #d4edda; color: #155724; padding: 10px; border-radius: 5px; margin: 10px 0;">
            {{ session('success') }}
        </div>
    @endif

    @if($tasks->count() > 0)
        <table>
            <thead>
                <tr>
                    <th>#</th>
                    <th>Task Name</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                @foreach($tasks as $task)
                    <tr>
                        <td>{{ $loop->iteration }}</td>
                        <td>{{ $task->name }}</td>
                        <td>
                            <form action="{{ route('tasks.destroy', $task->id) }}" method="POST" style="display: inline;">
                                @csrf
                                @method('DELETE')
                                <button type="submit" class="delete-btn" onclick="return confirm('Delete this task?')">
                                    X
                                </button>
                            </form>
                        </td>
                    </tr>
                @endforeach
            </tbody>
        </table>
    @else
        <div class="no-tasks">No tasks yet. Add one below!</div>
    @endif

    <div class="add-form">
        <form action="{{ route('tasks.store') }}" method="POST">
            @csrf
            <input type="text" name="name" placeholder="Enter new task..." required>
            <button type="submit">Add Task</button>
        </form>
    </div>
</body>

</html>