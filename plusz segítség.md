**Megjelíntéshez/felvevéshez**: php artisan make:model FájlNév -> protected $fillable = \['név'];



**Controller making** (így hoz alap dolgokat) php artisan make:controller controllerNév

Hozzáadni a hozzátartozókat pl: use App\\Models\\FájlNév;



**nyilván controllerbe a function-ket:** 
public function index()

&nbsp;   {

&nbsp;       $tasks = Task::all();

&nbsp;       return view('tasks', compact('tasks'));

&nbsp;   }



&nbsp;   public function store(Request $request)

&nbsp;   {

&nbsp;       $request->validate(\[

&nbsp;           'name' => 'required|string|max:255',

&nbsp;       ]);

&nbsp;       

&nbsp;       Task::create(\['name' => $request->name]);

&nbsp;       

&nbsp;       return redirect('/');

&nbsp;   }



&nbsp;   public function destroy($id)

&nbsp;   {

&nbsp;       Task::destroy($id);

&nbsp;       return redirect('/');

&nbsp;   }



**nyilván ezeket felvenni route-ba:** 
use App\\Http\\Controllers\\controllerNév;



Route::get('/', \[TaskController::class, 'index'])->name('tasks.index');

Route::post('/tasks', \[TaskController::class, 'store'])->name('tasks.store');

Route::delete('/tasks/{id}', \[TaskController::class, 'destroy'])->name('tasks.destroy');



**basic form hozzáadáshoz:**
    @if(session('success'))

&nbsp;       <div style="background: #d4edda; color: #155724; padding: 10px; border-radius: 5px; margin: 10px 0;">

&nbsp;           {{ session('success') }}

&nbsp;       </div>

&nbsp;   @endif

&nbsp;   

&nbsp;   @if($tasks->count() > 0)

&nbsp;       <table>

&nbsp;           <thead>

&nbsp;               <tr>

&nbsp;                   <th>#</th>

&nbsp;                   <th>Task Name</th>

&nbsp;                   <th>Action</th>

&nbsp;               </tr>

&nbsp;           </thead>

&nbsp;           <tbody>

&nbsp;               @foreach($tasks as $task)

&nbsp;               <tr>

&nbsp;                   <td>{{ $loop->iteration }}</td>

&nbsp;                   <td>{{ $task->name }}</td>

&nbsp;                   <td>

&nbsp;                       <form action="{{ route('tasks.destroy', $task->id) }}" method="POST" style="display: inline;">

&nbsp;                           @csrf

&nbsp;                           @method('DELETE')

&nbsp;                           <button type="submit" class="delete-btn" onclick="return confirm('Delete this task?')">

&nbsp;                               X

&nbsp;                           </button>

&nbsp;                       </form>

&nbsp;                   </td>

&nbsp;               </tr>

&nbsp;               @endforeach

&nbsp;           </tbody>

&nbsp;       </table>

&nbsp;   @else

&nbsp;       <div class="no-tasks">No tasks yet. Add one below!</div>

&nbsp;   @endif

&nbsp;   

&nbsp;   <div class="add-form">

&nbsp;       <form action="{{ route('tasks.store') }}" method="POST">

&nbsp;           @csrf

&nbsp;           <input type="text" name="name" placeholder="Enter new task..." required>

&nbsp;           <button type="submit">Add Task</button>

&nbsp;       </form>

&nbsp;   </div>





